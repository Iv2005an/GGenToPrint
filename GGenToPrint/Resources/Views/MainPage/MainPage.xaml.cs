using GGenToPrint.Resources.ViewModels;

namespace GGenToPrint.Resources.Views.MainPage;

public partial class MainPage : ContentPage
{
    readonly MainPageViewModel ViewModel;
    public MainPage(MainPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        viewModel.RefreshSettingsCommand.Execute(null);
        this.ViewModel = viewModel;
    }

    async void AddProfile(object sender, EventArgs args)
    {
        var profiles_count = ViewModel.Profiles.Count;

        var profileName = await DisplayPromptAsync(
            "Добавление профиля",
            "Введите имя профиля",
            "Добавить",
            "Отмена",
            "Имя профиля",
            50,
            initialValue: $"Профиль {profiles_count + 1}"
            );
        if (profileName is not null)
        {
            await ViewModel.AddProfileCommand.ExecuteAsync(profileName);
        }
    }

    async void DeleteProfile(object sender, EventArgs args)
    {
        var result = await DisplayAlert("Удаление профиля", $"Удалить {ViewModel.CurrentProfile.ProfileName}?", "Да", "Нет");
        if (result)
        {
            await ViewModel.DeleteProfileCommand.ExecuteAsync(null);
        }
    }
}