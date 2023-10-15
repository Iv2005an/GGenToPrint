using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using GGenToPrint.Resources.Models;

namespace GGenToPrint.Resources.ViewModels;

public partial class MainPageViewModel : ObservableObject
{
    [ObservableProperty]
    ObservableCollection<Profile> profiles;

    [ObservableProperty]
    byte selectedProfileIndex = 0;

    [ObservableProperty]
    byte numCellsOfVertical = 40;

    [ObservableProperty]
    byte numCellsOfHorizontal = 34;

    [ObservableProperty]
    byte numCellsOfMargin = 0;

    [ObservableProperty]
    byte sheetTypeIndex = 0;

    [ObservableProperty]
    byte sheetPositionIndex = 0;

    [ObservableProperty]
    byte cellSize = 5;

    [ObservableProperty]
    byte liftForMoving = 10;

    [ObservableProperty]
    bool unevennessOfWriting = false;
}