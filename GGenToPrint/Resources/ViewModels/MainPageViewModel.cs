using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using GGenToPrint.Resources.Models;
using GGenToPrint.Resources.Services;
using CommunityToolkit.Mvvm.Input;

namespace GGenToPrint.Resources.ViewModels;

public partial class MainPageViewModel : ObservableObject
{
    [ObservableProperty]
    ObservableCollection<Profile> profiles;

    [ObservableProperty]
    Profile currentProfile;

    [ObservableProperty]
    string profileName;

    [ObservableProperty]
    byte selectedProfileIndex;

    [ObservableProperty]
    byte numCellsOfVertical;

    [ObservableProperty]
    byte numCellsOfHorizontal;

    [ObservableProperty]
    byte numCellsOfMargin;

    [ObservableProperty]
    byte sheetTypeIndex;

    [ObservableProperty]
    byte sheetPositionIndex;

    [ObservableProperty]
    byte cellSize;

    [ObservableProperty]
    byte liftForMoving;

    [ObservableProperty]
    bool unevennessOfWriting;

    [RelayCommand]
    async Task RefreshSettings()
    {
        var profiles = await ProfileService.GetProfiles();
        Profiles = new ObservableCollection<Profile>(profiles);
        var currentProfile = profiles.Where(
            profile => profile.CurrentProfile).FirstOrDefault();
        if (currentProfile is not null)
        {
            CurrentProfile = currentProfile;
            ProfileName = currentProfile.ProfileName;
            SelectedProfileIndex = (byte)(currentProfile.ProfileId - 1);
            NumCellsOfVertical = currentProfile.NumCellsOfVertical;
            NumCellsOfHorizontal = currentProfile.NumCellsOfHorizontal;
            NumCellsOfMargin = currentProfile.NumCellsOfMargin;
            SheetTypeIndex = currentProfile.SheetTypeIndex;
            SheetPositionIndex = currentProfile.SheetPositionIndex;
            CellSize = currentProfile.CellSize;
            LiftForMoving = currentProfile.LiftForMoving;
            bool unevennessOfWriting = currentProfile.UnevennessOfWriting;
            UnevennessOfWriting = unevennessOfWriting;
        }
    }

    [RelayCommand]
    async Task AddProfile(string profileName)
    {
        CurrentProfile.CurrentProfile = false;
        var newProfile = CurrentProfile;
        newProfile.ProfileName = profileName;
        newProfile.CurrentProfile = true;
        await ProfileService.AddProfile(newProfile);
        await RefreshSettingsCommand.ExecuteAsync(null);
    }

    async partial void OnSelectedProfileIndexChanged(byte value)
    {
        await ProfileService.ChangeCurrentProfile(value);
    }
}