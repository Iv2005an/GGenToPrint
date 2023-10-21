using GGenToPrint.Resources.Models;
using SQLite;

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

    public static async Task<IEnumerable<Letter>> GetLetters(byte fontId)
    {
        await Init();

        return await Connection.Table<Letter>().Where(
            letter => letter.FontId == fontId).ToListAsync();
    }
    public static async Task AddLetter(Letter letter)
    {
        await Init();

        await Connection.InsertAsync(letter);
    }
    public static async Task DeleteLetter(Letter letter)
    {
        await Init();

        await Connection.DeleteAsync(letter);
    }
    public static async Task UpdateLetter(Letter updatedLetter)
    {
        await Connection.UpdateAsync(updatedLetter);
    }
}