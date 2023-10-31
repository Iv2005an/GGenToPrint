using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GGenToPrint.Resources.Models;
using GGenToPrint.Resources.Services;
using System.Collections.ObjectModel;
using Font = GGenToPrint.Resources.Models.Font;

namespace GGenToPrint.Resources.ViewModels;

public partial class FontPageViewModel : ObservableObject
{
    [ObservableProperty]
    ObservableCollection<Font> fonts;

    [ObservableProperty]
    Font currentFont;

    [ObservableProperty]
    string fontName;

    [RelayCommand]
    async Task AddFont(string fontName)
    {
        CurrentFont.FontName = fontName;
        await FontService.DisableCurrentFont();
        await FontService.AddFont(CurrentFont);
        await RefreshCommand.ExecuteAsync(null);
    }

    [RelayCommand]
    async Task DeleteFont()
    {
        await FontService.DeleteFont(CurrentFont);
        await RefreshCommand.ExecuteAsync(null);
    }

    [ObservableProperty]
    ObservableCollection<Letter> letters;

    [ObservableProperty]
    Letter currentLetter;

    [ObservableProperty]
    string commands;

    [RelayCommand]
    async Task Refresh()
    {
        Fonts = new(await FontService.GetFonts());
        CurrentFont = Fonts.Where(font => font.CurrentFont).FirstOrDefault();
    }

    [RelayCommand]
    async Task AddCharacter(string character)
    {
        await LetterService.AddLetter(new()
        {
            Character = character,
            FontId = CurrentFont.FontId
        });
        await RefreshCommand.ExecuteAsync(null);
    }

    [RelayCommand]
    async Task DeleteCharacter(Letter letter)
    {
        await LetterService.DeleteLetter(letter);
        await RefreshCommand.ExecuteAsync(null);
    }

    async partial void OnCurrentFontChanged(Font value)
    {
        if (value is not null)
        {
            CurrentLetter = null;
            FontName = value.FontName;
            await FontService.ChangeCurrentFont(value);
            Letters = new(await LetterService.GetLetters(value.FontId));
        }
    }

    partial void OnCurrentLetterChanged(Letter value)
    {
        if (value is not null) Commands = value.Commands;
        else Commands = null;
    }
}