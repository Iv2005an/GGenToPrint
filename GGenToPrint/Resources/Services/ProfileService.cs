﻿using GGenToPrint.Resources.Models;
using SQLite;

namespace GGenToPrint.Resources.Services;

public static class ProfileService
{
    static SQLiteAsyncConnection Database;

    static async Task Init()
    {
        if (Database is not null)
        {
            return;
        }
        Database = new SQLiteAsyncConnection(
            Path.Combine(FileSystem.AppDataDirectory, "GGenToPrint_profiles.db"));
        await Database.CreateTableAsync<Profile>();
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

        return await Database.Table<Profile>().ToListAsync();
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
        await DisableCurrentProfile();
        await Database.InsertAsync(profile);
    }

    public static async Task DeleteProfile(Profile profile)
    {
        await Init();

        await Database.DeleteAsync(profile);
        var profiles = (await GetProfiles()).ToList();
        if (profiles.Any())
        {
            for (byte profileIndex = 0; profileIndex < profiles.Count; profileIndex++)
            {
                profiles[profileIndex].ProfileId = profileIndex;
            }
            await Database.DeleteAllAsync<Profile>();
            profiles[0].CurrentProfile = true;
            await Database.InsertAllAsync(profiles);
        }
        else
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

    public static async Task DisableCurrentProfile()
    {
        await Init();

        var profiles = await GetProfiles();
        var oldCurrentProfile = profiles.Where(profile => profile.CurrentProfile).FirstOrDefault();
        if (oldCurrentProfile is not null)
        {
            oldCurrentProfile.CurrentProfile = false;
            await Database.UpdateAsync(oldCurrentProfile);
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
            await Database.UpdateAsync(newCurrentProfile);
        }
    }

    public static async Task UpdateProfile(Profile updatedProfile)
    {
        await Database.UpdateAsync(updatedProfile);
    }
}