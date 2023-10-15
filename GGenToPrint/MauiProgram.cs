﻿using Microsoft.Extensions.Logging;

namespace GGenToPrint;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder().UseMauiApp<App>();
#if DEBUG
        builder.Logging.AddDebug();
#endif
        return builder.Build();
    }
}