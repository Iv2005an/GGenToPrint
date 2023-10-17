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
        _db = new SQLiteAsyncConnection(
            Path.Combine(FileSystem.AppDataDirectory, "GGenToPrint_profiles.db"));
        await _db.CreateTableAsync<Profile>();
        if (!(await GetProfiles()).Any())
        {
            await AddProfile(new()
            {
                ProfileName = "Профиль 1",
                CurrentProfile = true,
                NumCellsOfVertical = 40,
                NumCellsOfHorizontal = 34,
                NumCellsOfMargin = 4,
                SheetTypeIndex = 0,
                SheetPositionIndex = 0,
                CellSize = 5,
                LiftForMoving = 10,
                UnevennessOfWriting = false
            });
        }
    }

    public static async Task<IEnumerable<Profile>> GetProfiles()
    {
        await Init();

        return await _db.Table<Profile>().ToListAsync();
    }

    public static async Task AddProfile(Profile profile)
    {
        await Init();

        var profiles = await GetProfiles();
        await DisableCurrentProfile();
        profile.ProfileId = (byte)(profiles.Count() + 1);
        await _db.InsertAsync(profile);
    }

    public static async Task DisableCurrentProfile()
    {
        await Init();

        var profiles = await GetProfiles();
        var oldCurrentProfile = profiles.Where(profile => profile.CurrentProfile).FirstOrDefault();
        if (oldCurrentProfile is not null)
        {
            oldCurrentProfile.CurrentProfile = false;
            await _db.UpdateAsync(oldCurrentProfile);
        }
    }

    public static async Task ChangeCurrentProfile(byte profileId)
    {
        await Init();

        await DisableCurrentProfile();

        var profiles = await GetProfiles();
        var newCurrentProfile = profiles.Where(
            profile => profile.ProfileId == profileId).FirstOrDefault();
        if (newCurrentProfile is not null)
        {
            newCurrentProfile.CurrentProfile = true;
            await _db.UpdateAsync(newCurrentProfile);
        }
    }

    public static async Task UpdateProfile(Profile updatedProfile)
    {
        await _db.UpdateAsync(updatedProfile);
    }
}