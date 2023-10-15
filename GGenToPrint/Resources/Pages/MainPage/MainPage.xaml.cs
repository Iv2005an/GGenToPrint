using GGenToPrint.Resources.ViewModels;

namespace GGenToPrint.Resources.Pages.MainPage;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}