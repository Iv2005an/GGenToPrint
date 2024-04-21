using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using GGenToPrint.Resources.Models;
using GGenToPrint.Resources.Databases;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Maui.Storage;
using CommunityToolkit.Maui.Core.Primitives;
using CommunityToolkit.Maui.Behaviors;

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

    [ObservableProperty]
    string text;

    [ObservableProperty]
    ObservableCollection<Symbol> symbols;

    [ObservableProperty]
    string savePath;

    [RelayCommand]
    async Task Refresh()
    {
        Profiles = new(await (await ProfilesDatabase.GetInstance()).GetProfiles());
        CurrentProfile = Profiles.Where(profile => profile.CurrentProfile).FirstOrDefault();
    }

    [RelayCommand]
    async Task AddProfile(string profileName)
    {
        CurrentProfile.ProfileName = profileName;
        ProfilesDatabase profileDatabase = await ProfilesDatabase.GetInstance();
        await profileDatabase.DisableCurrentProfile();
        await profileDatabase.AddProfile(CurrentProfile);
        await RefreshCommand.ExecuteAsync(null);
    }

    [RelayCommand]
    async Task DeleteProfile()
    {
        await (await ProfilesDatabase.GetInstance()).DeleteProfile(CurrentProfile);
        await RefreshCommand.ExecuteAsync(null);
    }

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

    [RelayCommand]
    async Task SaveGCode()
    {
        string gCode = ";Текст:\n";
        foreach (string line in Text.Split('\n'))
        {
            string commentLine = line.TrimEnd();
            if (!string.IsNullOrEmpty(commentLine))
                gCode += ';' + line.TrimEnd() + '\n';
        }

        gCode += "G21 ;Метрическая система координат\n";
        gCode += "G90 ;Абсолютная система координат\n";
        gCode += "G4 S5 ;Пауза 5 секунд\n";
        gCode += "G92 X0 Y0 Z0 ;Обнуление позиции\n";

        float xOffset = CellSize * 2;
        float yOffset = 0;
        foreach (char s in Text)
        {
            switch (s)
            {
                case ' ':
                    yOffset += CellSize * 1;
                    break;
                case '\t':
                    yOffset += CellSize * 4;
                    break;
                case '\n':
                    xOffset += CellSize * 2;
                    yOffset = 0;
                    break;
                default:
                    float maxY = 0;
                    Symbol currentSymbol = Symbols.FirstOrDefault(symbol => symbol.Sign == s.ToString());
                    if (currentSymbol is not null)
                    {
                        gCode += $"G0 Z{LiftForMoving} F6000\n";
                        foreach (GCommand gCommand in GCommand.ParseCommands(currentSymbol.GCode))
                        {
                            float x = gCommand.XCoordinate * CellSize + xOffset;
                            float y = gCommand.YCoordinate * CellSize + yOffset;

                            if (maxY < gCommand.YCoordinate) maxY = gCommand.YCoordinate;

                            if (gCommand.GType == 0)
                            {
                                gCode += $"G0 X{x:0.00} Y{y:0.00} Z{LiftForMoving} F6000\n";
                                gCode += $"G0 X{x:0.00} Y{y:0.00} Z0 F6000\n";
                            }
                            else if (gCommand.GType == 1)
                            {
                                gCode += $"G1 X{x:0.00} Y{y:0.00} Z0 F3600\n";
                            }
                        }
                        yOffset += maxY * CellSize + CellSize / 4;
                    }
                    break;
            }
        }
        gCode += $"G0 Z{LiftForMoving} F6000";

        DateTime dateTime = DateTime.Now;
        string path = Path.Combine(SavePath, $"{dateTime.Day}-{dateTime.Month}-{dateTime.Year} {dateTime.Hour}-{dateTime.Minute}-{dateTime.Second}.gcode");

        await File.WriteAllTextAsync(path, gCode);
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
            await (await ProfilesDatabase.GetInstance()).ChangeCurrentProfile(value);
        }
    }
    async partial void OnSavePathChanged(string value)
    {
        SavePath = value.Trim();
        CurrentProfile.SavePath = SavePath;
        await (await ProfilesDatabase.GetInstance()).UpdateProfile(CurrentProfile);
    }
    async partial void OnNumCellsOfVerticalChanged(byte value)
    {
        if (CurrentProfile is null) return;
        CurrentProfile.NumCellsOfVertical = value;
        await (await ProfilesDatabase.GetInstance()).UpdateProfile(CurrentProfile);
    }
    async partial void OnNumCellsOfHorizontalChanged(byte value)
    {
        if (CurrentProfile is null) return;
        CurrentProfile.NumCellsOfHorizontal = value;
        await (await ProfilesDatabase.GetInstance()).UpdateProfile(CurrentProfile);
    }
    async partial void OnNumCellsOfMarginChanged(byte value)
    {
        if (CurrentProfile is null) return;
        CurrentProfile.NumCellsOfMargin = value;
        await (await ProfilesDatabase.GetInstance()).UpdateProfile(CurrentProfile);
    }
    async partial void OnSheetTypeIndexChanged(byte value)
    {
        if (CurrentProfile is null) return;
        CurrentProfile.SheetTypeIndex = value;
        await (await ProfilesDatabase.GetInstance()).UpdateProfile(CurrentProfile);
    }
    async partial void OnSheetPositionIndexChanged(byte value)
    {
        if (CurrentProfile is null) return;
        CurrentProfile.SheetPositionIndex = value;
        await (await ProfilesDatabase.GetInstance()).UpdateProfile(CurrentProfile);
    }
    async partial void OnCellSizeChanged(byte value)
    {
        if (CurrentProfile is null) return;
        CurrentProfile.CellSize = value;
        await (await ProfilesDatabase.GetInstance()).UpdateProfile(CurrentProfile);
    }
    async partial void OnLiftForMovingChanged(byte value)
    {
        if (CurrentProfile is null) return;
        CurrentProfile.LiftForMoving = value;
        await (await ProfilesDatabase.GetInstance()).UpdateProfile(CurrentProfile);
    }
    async partial void OnUnevennessOfWritingChanged(bool value)
    {
        if (CurrentProfile is null) return;
        CurrentProfile.UnevennessOfWriting = value;
        await (await ProfilesDatabase.GetInstance()).UpdateProfile(CurrentProfile);
    }
    async partial void OnTextChanged(string value)
    {
        Symbols = new ObservableCollection<Symbol>(await (await SymbolsDatabase.GetInstance()).GetSymbols((await (await FontsDatabase.GetInstance()).GetCurrentFont()).FontId));
    }
}