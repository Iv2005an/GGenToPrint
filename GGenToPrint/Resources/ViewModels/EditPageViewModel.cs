using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GGenToPrint.Resources.Models;
using GGenToPrint.Resources.Databases;

namespace GGenToPrint.Resources.ViewModels;

public partial class EditPageViewModel : ObservableObject
{
    [ObservableProperty]
    Symbol currentSymbol;

    [ObservableProperty]
    string sign;

    [ObservableProperty]
    byte connectionTypeIndex;

    [ObservableProperty]
    float left;

    [ObservableProperty]
    float top;

    [ObservableProperty]
    float cellSize;

    [ObservableProperty]
    string symbolGCode;

    PointF GetEditSymbolGridCoordinate(PointF coordinates)
    {
        return new PointF(
            (float)Math.Round((coordinates.Y - Top) / CellSize - 2, 2),
            (float)Math.Round((coordinates.X - Left) / CellSize - 2, 2));
    }

    bool IsEqualCoordinates(PointF coordinates)
    {
        bool equalsCommands = false;
        if (SymbolGCode != null)
        {
            GCommand lastCommand = GCommand.ParseCommands(SymbolGCode).LastOrDefault();
            PointF editSymbolGridCoordinates = GetEditSymbolGridCoordinate(coordinates);
            equalsCommands = lastCommand.XCoordinate == editSymbolGridCoordinates.X && lastCommand.YCoordinate == editSymbolGridCoordinates.Y;
        }
        return equalsCommands;
    }

    void AddGCodeCommand(PointF coordinates, byte gCommandType)
    {
        PointF editSymbolGridCoordinates = GetEditSymbolGridCoordinate(coordinates);
        if (editSymbolGridCoordinates.X >= -2 && editSymbolGridCoordinates.X <= 2 &&
            editSymbolGridCoordinates.Y >= -2 && editSymbolGridCoordinates.Y <= 2)
        {
            if (!IsEqualCoordinates(coordinates))
                SymbolGCode += $"G{gCommandType} X{editSymbolGridCoordinates.X:0.00} Y{editSymbolGridCoordinates.Y:0.00}\n";
        }
    }

    [RelayCommand]
    void StartGCodeChanging(PointF coordinates)
    {
        AddGCodeCommand(coordinates, 0);
    }

    [RelayCommand]
    void GCodeChanging(PointF coordinates)
    {
        AddGCodeCommand(coordinates, 1);
    }

    [RelayCommand]
    void Cancel()
    {
        SymbolGCode = CurrentSymbol.GCode;
    }

    [RelayCommand]
    void Clear()
    {
        SymbolGCode = null;
    }

    [RelayCommand]
    async Task Save()
    {
        CurrentSymbol.GCode = SymbolGCode;
        await (await SymbolsDatabase.GetInstance()).UpdateSymbol(CurrentSymbol);
    }

    partial void OnCurrentSymbolChanged(Symbol value)
    {
        Sign = value.Sign;
        SymbolGCode = value.GCode;
        ConnectionTypeIndex = value.ConnectionType;
    }

    async partial void OnConnectionTypeIndexChanged(byte value)
    {
        CurrentSymbol.ConnectionType = value;
        await (await SymbolsDatabase.GetInstance()).UpdateSymbol(CurrentSymbol);
    }
}