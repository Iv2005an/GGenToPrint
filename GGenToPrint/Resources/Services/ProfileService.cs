using GGenToPrint.Resources.Models;
using SQLite;

namespace GGenToPrint.Resources.Services;

public static class ProfileService
{
    static SQLiteAsyncConnection Connection;

    static async Task Init()
    {
        if (Connection is not null)
        {
            return;
        }
        Connection = Database.Connection;
        await Connection.CreateTableAsync<Profile>();
        if (!(await GetProfiles()).Any())
        {
            await AddProfile(new());
        }
    }

    public static async Task<IEnumerable<Profile>> GetProfiles()
    {
        await Init();

        return await Connection.Table<Profile>().ToListAsync();
    }

    public static async Task AddProfile(Profile profile)
    {
        await Init();

        var profiles = await GetProfiles();
        if (profiles.Any())
        {
            profile.ProfileId = (byte)(profiles.LastOrDefault().ProfileId + 1);
        }
        else
        {
            profile.ProfileId = 0;
        }
        await Connection.InsertAsync(profile);
    }
    
    public static async Task UpdateProfile(Profile updatedProfile)
    {
        await Connection.UpdateAsync(updatedProfile);
    }

    public static async Task DeleteProfile(Profile profile)
    {
        await Init();

        await Connection.DeleteAsync(profile);
        var profiles = (await GetProfiles()).ToList();
        if (profiles.Any())
        {
            for (byte profileIndex = 0; profileIndex < profiles.Count; profileIndex++)
            {
                profiles[profileIndex].ProfileId = profileIndex;
            }
            profiles[0].CurrentProfile = true;
            await Connection.DeleteAllAsync<Profile>();
            await Connection.InsertAllAsync(profiles);
        }
        else
        {
            await AddProfile(new());
        }
    }

    public static async Task DisableCurrentProfile()
    {
        await Init();

        var profiles = await GetProfiles();
        var oldCurrentProfile = profiles.Where(profile => profile.CurrentProfile).FirstOrDefault();
        if (oldCurrentProfile is not null)
        {
            oldCurrentProfile.CurrentProfile = false;
            await Connection.UpdateAsync(oldCurrentProfile);
        }
    }

    public static async Task ChangeCurrentProfile(Profile newCurrentProfile)
    {
        await Init();

        await DisableCurrentProfile();
        newCurrentProfile.CurrentProfile = true;
        await Connection.UpdateAsync(newCurrentProfile);
    }
}