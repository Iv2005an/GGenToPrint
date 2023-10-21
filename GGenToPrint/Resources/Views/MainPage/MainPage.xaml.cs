using GGenToPrint.Resources.ViewModels;

namespace GGenToPrint.Resources.Views.MainPage;

public partial class MainPage : ContentPage
{
    readonly MainPageViewModel ViewModel;
    public MainPage(MainPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        this.ViewModel = viewModel;
        ViewModel.RefreshCommand.Execute(null);
    }

    async void AddProfile(object sender, EventArgs args)
    {
        var profilesCount = ViewModel.Profiles.Count;
        if (profilesCount < 256)
        {

            var profileName = await DisplayPromptAsync(
                "Добавление профиля",
                "Введите имя профиля",
                "Добавить",
                "Отмена",
                "Имя профиля",
                30,
                initialValue: $"Профиль {profilesCount + 1}"
                );
            if (profileName is not null)
            {
                profileName = profileName.Trim();
                if (profileName.Length > 0)
                {
                await ViewModel.AddProfileCommand.ExecuteAsync(profileName);
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

    async void DeleteProfile(object sender, EventArgs args)
    {
        if (await DisplayAlert("Удаление профиля", $"Удалить {ViewModel.CurrentProfile.ProfileName}?", "Да", "Нет"))
        {
            await ViewModel.DeleteProfileCommand.ExecuteAsync(null);
        }
    }
}