using GGenToPrint.Resources.Services;

namespace GGenToPrint;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }

    protected override Window CreateWindow(IActivationState activationState)
    {
        Window window = base.CreateWindow(activationState);
        window.Title = AppInfo.Current.Name;
        window.MinimumWidth = 1200;
        window.MinimumHeight= 900;
        return window;
    }
}