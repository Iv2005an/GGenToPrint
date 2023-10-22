using GGenToPrint.Resources.Models;
using SQLite;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GGenToPrint.Resources.Services;

public static class LetterService
{
    static SQLiteAsyncConnection Connection;

    static async Task Init()
    {
        if (Connection is not null)
        {
            return;
        }
        Connection = Database.Connection;
        await Connection.CreateTableAsync<Letter>();
    }

    public static async Task<IEnumerable<Letter>> GetAllLetters()
    {
        return await Connection.Table<Letter>().ToListAsync();
    }

    public static async Task<IEnumerable<Letter>> GetLetters(byte fontId)
    {
        await Init();

        return await Connection.Table<Letter>().Where(
            letter => letter.FontId == fontId).ToListAsync();
    }
    public static async Task AddLetter(Letter letter)
    {
        await Init();
        var letters = await GetAllLetters();
        if (letters.Any())
        {
            letter.LetterId = (byte)(letters.LastOrDefault().LetterId + 1);
        }
        else
        {
            letter.LetterId = 0;
        }
        await Connection.InsertAsync(letter);
    }
    public static async Task DeleteLetter(Letter letter)
    {
        await Init();

        await Connection.DeleteAsync(letter);
        var letters = (await GetAllLetters()).ToList();
        if (letters.Any())
        {
            for (int letterIndex = 0; letterIndex < letters.Count; letterIndex++)
            {
                letters[letterIndex].LetterId = letterIndex;
            }
            await Connection.DeleteAllAsync<Letter>();
            await Connection.InsertAllAsync(letters);
        }
    }
    public static async Task UpdateLetter(Letter updatedLetter)
    {
        await Connection.UpdateAsync(updatedLetter);
    }
}