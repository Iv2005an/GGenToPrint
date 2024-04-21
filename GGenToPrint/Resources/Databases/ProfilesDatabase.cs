using GGenToPrint.Resources.Models;
using SQLite;

namespace GGenToPrint.Resources.Databases;

public class ProfilesDatabase
{
    private ProfilesDatabase() { }

    private static ProfilesDatabase instance;

    async public static Task<ProfilesDatabase> GetInstance()
    {
        if (instance == null)
        {
            instance = new ProfilesDatabase();
            await instance.Init();
        }
        return instance;
    }
    private SQLiteAsyncConnection Connection;

    private async Task Init()
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

    public async Task<IEnumerable<Profile>> GetProfiles()
    {
        return await Connection.Table<Profile>().ToListAsync();
    }

    public async Task AddProfile(Profile profile)
    {
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

    public async Task UpdateProfile(Profile updatedProfile)
    {
        await Connection.UpdateAsync(updatedProfile);
    }

    public async Task DeleteProfile(Profile profile)
    {
        await Connection.DeleteAsync(profile);
        var profiles = (await GetProfiles()).ToList();
        if (profiles.Count != 0)
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

    public async Task DisableCurrentProfile()
    {
        var profiles = await GetProfiles();
        var oldCurrentProfile = profiles.FirstOrDefault(profile => profile.CurrentProfile);
        if (oldCurrentProfile is not null)
        {
            oldCurrentProfile.CurrentProfile = false;
            await Connection.UpdateAsync(oldCurrentProfile);
        }
    }

    public async Task ChangeCurrentProfile(Profile newCurrentProfile)
    {
        await DisableCurrentProfile();
        newCurrentProfile.CurrentProfile = true;
        await Connection.UpdateAsync(newCurrentProfile);
    }
}