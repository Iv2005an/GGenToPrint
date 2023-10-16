using GGenToPrint.Resources.ViewModels;

namespace GGenToPrint.Resources.Views.MainPage;

public partial class MainPage : ContentPage
{
    readonly MainPageViewModel vm;
    public MainPage(MainPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        vm.RefreshSettingsCommand.Execute(null);
        this.vm = vm;
    }

    async void AddProfile(object sender, EventArgs args)
    {
        var profiles_count = vm.Profiles.Count;

        var profileName = await DisplayPromptAsync(
            "Добавление профиля",
            "Введите имя профиля",
            "Добавить",
            "Отмена",
            "Имя профиля",
            50,
            initialValue: $"Профиль {profiles_count + 1}"
            );
        await vm.AddProfileCommand.ExecuteAsync(profileName);
    }
}