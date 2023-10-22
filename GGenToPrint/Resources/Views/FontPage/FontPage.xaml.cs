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
                    await DisplayAlert("Ошибка", "Пустое имя пользователя", "ОК");
                }
            }
        }
        else
        {
            await DisplayAlert("Ошибка", "Превышено количество пользователей", "ОК");
        }
    }
    async void DeleteFont(object sender, EventArgs args)
    {
        if (await DisplayAlert("Удаление шрифта", $"Удалить {ViewModel.CurrentFont.FontName}?", "Да", "Нет"))
        {
            await ViewModel.DeleteFontCommand.ExecuteAsync(null);
        }
    }

    async void AddCharacter(object sender, EventArgs args)
    {
        var character = await DisplayPromptAsync(
            "Добавление символа",
            "Введите символ",
            "Добавить",
            "Отмена",
            "Символ", 1
            );
        if (character is not null)
        {
            if (character != string.Empty)
            {
                if (!ViewModel.Letters.Where(letter => letter.Character == character).Any())
                {
                    ViewModel.AddCharacterCommand.Execute(character);
                }
                else
                {
                    await DisplayAlert("Ошибка", "Введен повторяющийся символ", "ОК");
                }
            }
            else
            {
                await DisplayAlert("Ошибка", "Не введён символ", "ОК");
            }
        }
    }

    async void DeleteCharacter(object sender, EventArgs args)
    {
        var letter = (Letter)((Button)sender).BindingContext;
        if (await DisplayAlert("Удаление символа", $"Удалить символ \"{letter.Character}\"?", "Да", "Нет"))
        {
            await ViewModel.DeleteCharacterCommand.ExecuteAsync(letter);
        }
    }
    async void ChangeCharacter(object sender, EventArgs args)
    {

    }
}