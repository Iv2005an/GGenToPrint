using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GGenToPrint.Resources.Models;
using GGenToPrint.Resources.Databases;
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

    [ObservableProperty]
    ObservableCollection<Symbol> symbols;

    [ObservableProperty]
    Symbol currentSymbol;

    [ObservableProperty]
    string symbolGCode;

    [RelayCommand]
    async Task Refresh()
    {
        Fonts = new(await (await FontsDatabase.GetInstance()).GetFonts());
        CurrentFont = Fonts.Where(font => font.CurrentFont).FirstOrDefault();
    }

    [RelayCommand]
    async Task AddFont(string fontName)
    {
        CurrentFont.FontName = fontName;
        await (await FontsDatabase.GetInstance()).DisableCurrentFont();
        await (await FontsDatabase.GetInstance()).AddFont(CurrentFont);
        await RefreshCommand.ExecuteAsync(null);
    }

    [RelayCommand]
    async Task DeleteFont()
    {
        await (await FontsDatabase.GetInstance()).DeleteFont(CurrentFont);
        await RefreshCommand.ExecuteAsync(null);
    }

    [RelayCommand]
    async Task AddSymbol(string symbol)
    {
        await (await SymbolsDatabase.GetInstance()).AddSymbol(new()
        {
            Sign = symbol,
            FontId = CurrentFont.FontId
        });
        await RefreshCommand.ExecuteAsync(null);
    }

    [RelayCommand]
    async Task DeleteSymbol(Symbol symbol)
    {
        await (await SymbolsDatabase.GetInstance()).DeleteSymbol(symbol);
        await RefreshCommand.ExecuteAsync(null);
    }

    async partial void OnCurrentFontChanged(Font value)
    {
        if (value is not null)
        {
            CurrentSymbol = null;
            FontName = value.FontName;
            await (await FontsDatabase.GetInstance()).ChangeCurrentFont(value);
            Symbols = new(await (await SymbolsDatabase.GetInstance()).GetSymbols(value.FontId));
        }
    }

    partial void OnCurrentSymbolChanged(Symbol value)
    {
        if (value is not null) SymbolGCode = value.GCode;
        else SymbolGCode = null;
    }
}