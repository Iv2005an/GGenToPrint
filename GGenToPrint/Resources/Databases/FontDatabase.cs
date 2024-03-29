using SQLite;
using Font = GGenToPrint.Resources.Models.Font;

namespace GGenToPrint.Resources.Services;

public class FontDatabase
{
    private FontDatabase() { }

    private static FontDatabase instance;

    async public static Task<FontDatabase> GetInstance()
    {
        if (instance == null)
        {
            instance = new FontDatabase();
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
        return (await GetFonts()).Where(font => font.CurrentFont).FirstOrDefault();
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
        var letters = await (await LetterDatabase.GetInstance()).GetAllLetters();
        foreach (var letter in letters)
        {
            if (letter.FontId == font.FontId)
            {
                await (await LetterDatabase.GetInstance()).DeleteLetter(letter);
            }
            else if (letter.FontId > font.FontId)
            {
                await (await LetterDatabase.GetInstance()).DeleteLetter(letter);
                letter.FontId = (byte)(letter.FontId - 1);
                await (await LetterDatabase.GetInstance()).AddLetter(letter);
            }
        }
    }

    public async Task DisableCurrentFont()
    {
        var oldCurrentFont = (await GetFonts()).Where(font => font.CurrentFont).FirstOrDefault();
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