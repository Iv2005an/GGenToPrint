using SQLite;
using Font = GGenToPrint.Resources.Models.Font;

namespace GGenToPrint.Resources.Services;

public static class FontService
{
    static SQLiteAsyncConnection Connection;
    static async Task Init()
    {
        if (Connection is not null)
        {
            return;
        }
        Connection = Database.Connection;
        await Connection.CreateTableAsync<Font>();
        if (!(await GetFonts()).Any())
        {
            await AddFont(new());
        }
    }

    public static async Task<IEnumerable<Font>> GetFonts()
    {
        await Init();

        return await Connection.Table<Font>().ToListAsync();
    }

    public static async Task<Font> GetCurrentFont()
    {
        await Init();

        return (await GetFonts()).Where(font => font.CurrentFont).FirstOrDefault();
    }

    public static async Task AddFont(Font font)
    {
        await Init();

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

    public static async Task DeleteFont(Font font)
    {
        await Init();

        await Connection.DeleteAsync(font);
        var fonts = (await GetFonts()).ToList();
        if (fonts.Any())
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
        var letters = await LetterService.GetAllLetters();
        foreach (var letter in letters)
        {
            if (letter.FontId == font.FontId)
            {
                await LetterService.DeleteLetter(letter);
            }
            else if (letter.FontId > font.FontId)
            {
                await LetterService.DeleteLetter(letter);
                letter.FontId = (byte)(letter.FontId - 1);
                await LetterService.AddLetter(letter);
            }
        }
    }

    public static async Task DisableCurrentFont()
    {
        await Init();

        var fonts = await GetFonts();
        var oldCurrentFont = fonts.Where(font => font.CurrentFont).FirstOrDefault();
        if (oldCurrentFont is not null)
        {
            oldCurrentFont.CurrentFont = false;
            await Connection.UpdateAsync(oldCurrentFont);
        }
    }

    public static async Task ChangeCurrentFont(Font newCurrentFont)
    {
        await Init();

        await DisableCurrentFont();
        newCurrentFont.CurrentFont = true;
        await Connection.UpdateAsync(newCurrentFont);
    }
}