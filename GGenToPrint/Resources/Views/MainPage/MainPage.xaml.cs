using GGenToPrint.Resources.ViewModels;

namespace GGenToPrint.Resources.Views.MainPage;

public partial class MainPage : ContentPage
{
    readonly MainPageViewModel ViewModel;
    public MainPage(MainPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        ViewModel = viewModel;
        ViewModel.RefreshCommand.Execute(null);
    }

    async void AddProfile(object sender, EventArgs args)
    {
        byte profilesCount = (byte)ViewModel.Profiles.Count;
        if (profilesCount < 255)
        {
            string profileName = await DisplayPromptAsync(
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

    async void SaveGCode(object sender, EventArgs args)
    {
        if (string.IsNullOrEmpty(ViewModel.SavePath))
        {
            await DisplayAlert("Ошибка", "Введите путь", "ОК");
            return;
        }
        if (string.IsNullOrEmpty(ViewModel.Text))
        {
            await DisplayAlert("Ошибка", "Введите текст", "ОК");
            return;
        }
        try
        {
            await ViewModel.SaveGCodeCommand.ExecuteAsync(null);
        }
        catch (IOException ex)
        {
            if (ex is DirectoryNotFoundException)
                await DisplayAlert("Ошибка", "Директория не найдена", "ОК");
            else if (ex is DriveNotFoundException)
                await DisplayAlert("Ошибка", "Диск не найден", "ОК");
            else
                await DisplayAlert("Ошибка", "Ошибка сохранения файла", "ОК");
            return;
        }
        await DisplayAlert("Сохранение", "Код сохранён в файл", "ОК");
    }
}