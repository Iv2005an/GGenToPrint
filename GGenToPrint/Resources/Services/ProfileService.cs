using GGenToPrint.Resources.Models;
using SQLite;

namespace GGenToPrint.Resources.Services;

public static class ProfileService
{
    static SQLiteAsyncConnection _db;

    static async Task Init()
    {
        if (_db is not null)
        {
            return;
        }
        _db = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, "profiles.db"), SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        await _db.CreateTableAsync<Profile>();
        await AddProfile(new()
        {
            ProfileName = "Профиль",
            NumCellsOfVertical = 40,
            NumCellsOfHorizontal = 34,
            NumCellsOfMargin = 4,
            SheetTypeIndex = 0,
            SheetPositionIndex = 0,
            CellSize = 5,
            LiftForMoving = 10,
            UnevennessOfWriting = true
        });
    }

    public static async Task AddProfile(Profile profile)
    {
        await Init();

        await _db.InsertAsync(profile);
    }

    public static async Task<IEnumerable<Profile>> GetProfiles()
    {
        await Init();

        return await _db.Table<Profile>().ToListAsync();
    }
}