using CommunityToolkit.Maui;
using FinanceManagerUI.Pages;
using FinanceManagerUI.ViewModels;
using Microsoft.Extensions.Logging;
using UraniumUI;

namespace FinanceManagerUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseUraniumUI()
                .UseUraniumUIMaterial()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // DEPENDENCY INJECTION
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MainViewModel>();

            builder.Services.AddTransient<AccountPage>();
            builder.Services.AddTransient<AccountViewModel>();

            builder.Services.AddTransient<ExpensesPage>();
            builder.Services.AddTransient<ExpensesViewModel>();

            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<LoginViewModel>();

            builder.Services.AddTransient<PropertiesPage>();
            builder.Services.AddTransient<PropertiesViewModel>();

            builder.Services.AddTransient<RegistrationPage>();
            builder.Services.AddTransient<RegistrationViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
