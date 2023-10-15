using GGenToPrint.Resources.Views.MainPage;
using GGenToPrint.Resources.ViewModels;
using Microsoft.Extensions.Logging;
using GGenToPrint.Resources.Services;

namespace GGenToPrint;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder().UseMauiApp<App>();
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<MainPageViewModel>();
#if DEBUG
        builder.Logging.AddDebug();
#endif
        return builder.Build();
    }
}