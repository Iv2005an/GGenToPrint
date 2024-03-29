using GGenToPrint.Resources.Models;
using SQLite;

namespace GGenToPrint.Resources.Services;

public class LetterDatabase
{
    private LetterDatabase() { }

    private static LetterDatabase instance;

    async public static Task<LetterDatabase> GetInstance()
    {
        if (instance == null)
        {
            instance = new LetterDatabase();
            await instance.Init();
        }
        return instance;
    }
    private SQLiteAsyncConnection Connection;

    private async Task Init()
    {
        Connection = Database.Connection;
        await Connection.CreateTableAsync<Letter>();
    }

    public async Task<IEnumerable<Letter>> GetAllLetters()
    {
        return await Connection.Table<Letter>().ToListAsync();
    }

    public async Task<IEnumerable<Letter>> GetLetters(byte fontId)
    {
        return await Connection.Table<Letter>().Where(
            letter => letter.FontId == fontId).ToListAsync();
    }
    public async Task AddLetter(Letter letter)
    {
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
    public async Task DeleteLetter(Letter letter)
    {
        await Connection.DeleteAsync(letter);
        var letters = (await GetAllLetters()).ToList();
        if (letters.Count != 0)
        {
            for (int letterIndex = 0; letterIndex < letters.Count; letterIndex++)
            {
                letters[letterIndex].LetterId = letterIndex;
            }
            await Connection.DeleteAllAsync<Letter>();
            await Connection.InsertAllAsync(letters);
        }
    }
    public async Task UpdateLetter(Letter updatedLetter)
    {
        await Connection.UpdateAsync(updatedLetter);
    }
}