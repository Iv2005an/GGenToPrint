using GGenToPrint.Resources.Models;
using GGenToPrint.Resources.ViewModels;

namespace GGenToPrint.Resources.Views.FontPage;

public partial class FontPage : ContentPage
{
    readonly FontPageViewModel ViewModel;
    public FontPage(FontPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        ViewModel = viewModel;
        ViewModel.RefreshCommand.Execute(null);
    }

    async void AddFont(object sender, EventArgs args)
    {
        var fontsCount = ViewModel.Fonts.Count;
        if (fontsCount < 256)
        {

            var fontName = await DisplayPromptAsync(
                "Добавление профиля",
                "Введите имя профиля",
                "Добавить",
                "Отмена",
                "Имя профиля",
                30,
                initialValue: $"Шрифт {fontsCount + 1}"
                );
            if (fontName is not null)
            {
                fontName = fontName.Trim();
                if (fontName.Length > 0)
                {
                    await ViewModel.AddFontCommand.ExecuteAsync(fontName);
                }
                else
                {
                    await DisplayAlert("Ошибка", "Введите имя шрифта", "ОК");
                }
            }
        }
        else
        {
            await DisplayAlert("Ошибка", "Превышено количество шрифтов", "ОК");
        }
    }
    async void DeleteFont(object sender, EventArgs args)
    {
        if (await DisplayAlert("Удаление шрифта", $"Удалить {ViewModel.CurrentFont.FontName}?", "Да", "Нет"))
        {
            await ViewModel.DeleteFontCommand.ExecuteAsync(null);
        }
    }

    async void AddSymbol(object sender, EventArgs args)
    {
        var symbolToAdd = (await DisplayPromptAsync(
            "Добавление символа",
            "Введите символ",
            "Добавить",
            "Отмена",
            "Символ", 1
            )).Trim();
        if (!string.IsNullOrEmpty(symbolToAdd))
        {
            if (!ViewModel.Symbols.Where(symbol => symbol.Sign == symbolToAdd).Any())
            {
                ViewModel.AddSymbolCommand.Execute(symbolToAdd);
            }
            else
            {
                await DisplayAlert("Ошибка", "Повторяющийся символ", "ОК");
            }
        }
        else
        {
            await DisplayAlert("Ошибка", "Введите символ", "ОК");
        }
    }
    async void DeleteSymbol(object sender, EventArgs args)
    {
        var symbol = (Symbol)((Button)sender).BindingContext;
        if (await DisplayAlert("Удаление символа", $"Удалить символ \"{symbol.Sign}\"?", "Да", "Нет"))
        {
            await ViewModel.DeleteSymbolCommand.ExecuteAsync(symbol);
        }
    }
    async void EditSymbol(object sender, EventArgs args)
    {
        var oldSymbol = (Frame)Symbols.Where(symbol => (
        (Frame)symbol).BorderColor != null).FirstOrDefault();
        if (oldSymbol is not null) oldSymbol.BorderColor = null;

        var navigationParameters = new Dictionary<string, object>
        {
            { "Symbol", (Symbol)((Button)sender).BindingContext }
        };
        ViewModel.CurrentSymbol = null;
        await Shell.Current.GoToAsync("editSymbol", navigationParameters);
    }

    void ShowSymbolSheet(object sender, TappedEventArgs args)
    {
        var oldSymbol = (Frame)Symbols.Where(symbol => (
        (Frame)symbol).BorderColor != null).FirstOrDefault();
        if (oldSymbol is not null) oldSymbol.BorderColor = null;
        var frame = (Frame)sender;
        frame.BorderColor = Application.AccentColor;
        var symbol = (Symbol)frame.BindingContext;
        ViewModel.CurrentSymbol = symbol;
    }
}