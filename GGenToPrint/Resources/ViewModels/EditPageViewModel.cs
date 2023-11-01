﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GGenToPrint.Resources.Models;
using GGenToPrint.Resources.Services;

namespace GGenToPrint.Resources.ViewModels;

public partial class EditPageViewModel : ObservableObject
{
    [ObservableProperty]
    Letter currentLetter;

    [ObservableProperty]
    string character;

    [ObservableProperty]
    string commands;

    [ObservableProperty]
    byte connectionTypeIndex;

    [ObservableProperty]
    float cellSize;

    [ObservableProperty]
    float top;

    bool IsEqualCommands(PointF newCoordinates)
    {
        bool equalsCommands = false;
        if (Commands != null)
        {
            var last_command = Gcommand.ParseCommands(Commands).LastOrDefault();
            var lastX = last_command.XCoordinate;
            var lastY = last_command.YCoordinate;
            var newX = (float)Math.Round(newCoordinates.X / CellSize - 2, 2);
            var newY = (float)Math.Round(newCoordinates.Y / CellSize, 2);
            equalsCommands = lastX == newX && lastY == newY;
        }
        return equalsCommands;
    }
    bool outOfBorders;
    [RelayCommand]
    void StartCommandsChanging(PointF coordinates)
    {
        float lineSize = CellSize / 10;
        if (coordinates.X - lineSize / 2 > 0 &&
            coordinates.X + lineSize / 2 < CellSize * 4 &&
            coordinates.Y - lineSize / 2 > Top &&
            coordinates.Y + lineSize / 2 < CellSize * 4 + Top)
        {
            if (!IsEqualCommands(coordinates))
                Commands += $"G0 X{coordinates.X / CellSize - 2:0.00} Y{(coordinates.Y - Top) / CellSize:0.00}\n";
            outOfBorders = false;
        }
    }

    [RelayCommand]
    void CommandsChanging(PointF coordinates)
    {
        float lineSize = CellSize / 10;
        if (!outOfBorders &&
            coordinates.X - lineSize / 2 > 0 &&
            coordinates.X + lineSize / 2 < CellSize * 4 &&
            coordinates.Y - lineSize / 2 > Top &&
            coordinates.Y + lineSize / 2 < CellSize * 4 + Top)
        {
            if (!IsEqualCommands(coordinates))
                Commands += $"G1 X{coordinates.X / CellSize - 2:0.00} Y{(coordinates.Y - Top) / CellSize:0.00}\n";
        }
        else
            outOfBorders = true;
    }

    [RelayCommand]
    void Cancel()
    {
        Commands = CurrentLetter.Commands;
    }

    [RelayCommand]
    void Clear()
    {
        Commands = null;
    }

    [RelayCommand]
    async Task Save()
    {
        CurrentLetter.Commands = Commands;
        await LetterService.UpdateLetter(CurrentLetter);
    }

    partial void OnCurrentLetterChanged(Letter value)
    {
        Character = value.Character;
        Commands = value.Commands;
        ConnectionTypeIndex = value.ConnectionType;
    }

    async partial void OnConnectionTypeIndexChanged(byte value)
    {
        CurrentLetter.ConnectionType = value;
        await LetterService.UpdateLetter(CurrentLetter);
    }
}