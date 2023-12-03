using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using GGenToPrint.Resources.Models;
using GGenToPrint.Resources.Services;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Maui.Storage;
using CommunityToolkit.Maui.Core.Primitives;

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
    async Task Refresh()
    {
        Profiles = new(await ProfileService.GetProfiles());
        CurrentProfile = Profiles.Where(profile => profile.CurrentProfile).FirstOrDefault();
    }

    [RelayCommand]
    async Task AddProfile(string profileName)
    {
        CurrentProfile.ProfileName = profileName;
        await ProfileService.DisableCurrentProfile();
        await ProfileService.AddProfile(CurrentProfile);
        await RefreshCommand.ExecuteAsync(null);
    }

    [RelayCommand]
    async Task DeleteProfile()
    {
        await ProfileService.DeleteProfile(CurrentProfile);
        await RefreshCommand.ExecuteAsync(null);
    }


    [ObservableProperty]
    string savePath;

    [RelayCommand]
    async Task SelectFolder()
    {
        Folder folder;
        if (SavePath is not null)
        {
            folder = (await FolderPicker.PickAsync(SavePath, new CancellationTokenSource().Token)).Folder;
        }
        else
        {
            folder = (await FolderPicker.PickAsync(new CancellationTokenSource().Token)).Folder;
        }
        if (folder is not null)
        {
            SavePath = folder.Path;
        }
    }

    async partial void OnCurrentProfileChanged(Profile value)
    {
        if (value is not null)
        {
            ProfileName = value.ProfileName;
            SavePath = value.SavePath;
            NumCellsOfVertical = value.NumCellsOfVertical;
            NumCellsOfHorizontal = value.NumCellsOfHorizontal;
            NumCellsOfMargin = value.NumCellsOfMargin;
            SheetTypeIndex = value.SheetTypeIndex;
            SheetPositionIndex = value.SheetPositionIndex;
            CellSize = value.CellSize;
            LiftForMoving = value.LiftForMoving;
            UnevennessOfWriting = value.UnevennessOfWriting;
            await ProfileService.ChangeCurrentProfile(value);
        }
    }
    async partial void OnSavePathChanged(string value)
    {
        SavePath = value.TrimStart();
        CurrentProfile.SavePath = SavePath;
        await ProfileService.UpdateProfile(CurrentProfile);
    }
    async partial void OnNumCellsOfVerticalChanged(byte value)
    {
        if (CurrentProfile is null) return;
        CurrentProfile.NumCellsOfVertical = value;
        await ProfileService.UpdateProfile(CurrentProfile);
    }
    async partial void OnNumCellsOfHorizontalChanged(byte value)
    {
        if (CurrentProfile is null) return;
        CurrentProfile.NumCellsOfHorizontal = value;
        await ProfileService.UpdateProfile(CurrentProfile);
    }
    async partial void OnNumCellsOfMarginChanged(byte value)
    {
        if (CurrentProfile is null) return;
        CurrentProfile.NumCellsOfMargin = value;
        await ProfileService.UpdateProfile(CurrentProfile);
    }
    async partial void OnSheetTypeIndexChanged(byte value)
    {
        if (CurrentProfile is null) return;
        CurrentProfile.SheetTypeIndex = value;
        await ProfileService.UpdateProfile(CurrentProfile);
    }
    async partial void OnSheetPositionIndexChanged(byte value)
    {
        if (CurrentProfile is null) return;
        CurrentProfile.SheetPositionIndex = value;
        await ProfileService.UpdateProfile(CurrentProfile);
    }
    async partial void OnCellSizeChanged(byte value)
    {
        if (CurrentProfile is null) return;
        CurrentProfile.CellSize = value;
        await ProfileService.UpdateProfile(CurrentProfile);
    }
    async partial void OnLiftForMovingChanged(byte value)
    {
        if (CurrentProfile is null) return;
        CurrentProfile.LiftForMoving = value;
        await ProfileService.UpdateProfile(CurrentProfile);
    }
    async partial void OnUnevennessOfWritingChanged(bool value)
    {
        if (CurrentProfile is null) return;
        CurrentProfile.UnevennessOfWriting = value;
        await ProfileService.UpdateProfile(CurrentProfile);
    }
}