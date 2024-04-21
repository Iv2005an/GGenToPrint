using SQLite;
using Font = GGenToPrint.Resources.Models.Font;

namespace GGenToPrint.Resources.Databases;

public class FontsDatabase
{
    private FontsDatabase() { }

    private static FontsDatabase instance;

    async public static Task<FontsDatabase> GetInstance()
    {
        if (instance == null)
        {
            instance = new FontsDatabase();
            await instance.Init();
        }
        return instance;
    }

    private SQLiteAsyncConnection Connection;

    private async Task Init()
    {
        Connection = Database.Connection;
        await Connection.CreateTableAsync<Font>();
        if (!(await GetFonts()).Any())
        {
            await AddFont(new());
        }
    }

    public async Task<IEnumerable<Font>> GetFonts()
    {
        return await Connection.Table<Font>().ToListAsync();
    }

    public async Task<Font> GetCurrentFont()
    {
        return (await GetFonts()).FirstOrDefault(font => font.CurrentFont);
    }

    public async Task AddFont(Font font)
    {
        var fonts = await GetFonts();
        if (fonts.Any())
        {
            font.FontId = (byte)(fonts.LastOrDefault().FontId + 1);
        }
        else
        {
            font.FontId = 0;
        }
        await Connection.InsertAsync(font);
    }

    public async Task DeleteFont(Font font)
    {
        await Connection.DeleteAsync(font);
        var fonts = (await GetFonts()).ToList();
        if (fonts.Count != 0)
        {
            for (byte fontIndex = 0; fontIndex < fonts.Count; fontIndex++)
            {
                fonts[fontIndex].FontId = fontIndex;
            }
            fonts[0].CurrentFont = true;
            await Connection.DeleteAllAsync<Font>();
            await Connection.InsertAllAsync(fonts);
        }
        else
        {
            await AddFont(new());
        }
        var symbols = await (await SymbolsDatabase.GetInstance()).GetAllSymbols();
        foreach (var symbol in symbols)
        {
            if (symbol.FontId == font.FontId)
            {
                await (await SymbolsDatabase.GetInstance()).DeleteSymbol(symbol);
            }
            else if (symbol.FontId > font.FontId)
            {
                await (await SymbolsDatabase.GetInstance()).DeleteSymbol(symbol);
                symbol.FontId = (byte)(symbol.FontId - 1);
                await (await SymbolsDatabase.GetInstance()).AddSymbol(symbol);
            }
        }
    }

    public async Task DisableCurrentFont()
    {
        var oldCurrentFont = (await GetFonts()).FirstOrDefault(font => font.CurrentFont);
        if (oldCurrentFont is not null)
        {
            oldCurrentFont.CurrentFont = false;
            await Connection.UpdateAsync(oldCurrentFont);
        }
    }

    public async Task ChangeCurrentFont(Font newCurrentFont)
    {
        await DisableCurrentFont();
        newCurrentFont.CurrentFont = true;
        await Connection.UpdateAsync(newCurrentFont);
    }
}