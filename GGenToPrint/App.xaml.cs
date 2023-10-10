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
        window.MinimumWidth = 1300;
        window.MinimumHeight= 300;
        return window;
    }
}

