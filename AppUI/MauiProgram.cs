using AppUI.Helpers;
using AppUI.Services;
using AppUI.State;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;

namespace AppUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddMudServices();
            builder.Services.AddHttpClient();

            builder.Services.AddScoped<PropertiesService>();
            builder.Services.AddScoped<StaticDataService>();

            // State DI
            builder.Services.AddSingleton<RouterState>();
            builder.Services.AddSingleton<UserState>();
            builder.Services.AddSingleton<StaticDataState>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
