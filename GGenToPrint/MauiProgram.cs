using GGenToPrint.Resources.Views.MainPage;
using GGenToPrint.Resources.Views.FontPage;
using GGenToPrint.Resources.Views.EditPage;
using GGenToPrint.Resources.ViewModels;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;

namespace GGenToPrint;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder().UseMauiApp<App>().UseMauiCommunityToolkit();
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<MainPageViewModel>();
        builder.Services.AddSingleton<FontPage>();
        builder.Services.AddSingleton<FontPageViewModel>();
        builder.Services.AddSingleton<EditPage>();
        builder.Services.AddSingleton<EditPageViewModel>();
#if DEBUG
        builder.Logging.AddDebug();
#endif
        return builder.Build();
    }
}