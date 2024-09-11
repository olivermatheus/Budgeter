using Budgeter.ViewModels;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;

namespace Budgeter
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>().ConfigureFonts(fonts =>
            {
                // fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("sujetbi_.ttf", "Sujeta");
            }).UseMauiCommunityToolkit();
#if DEBUG
            builder.Logging.AddDebug();
#endif
            var services = builder.Services;

            services.AddSingleton<MainViewModel>();
            services.AddSingleton<MainPage>(s => new MainPage() { BindingContext = s.GetRequiredService<MainViewModel>() });

            services.AddTransient<DetailPage>();
            services.AddTransient<DetailViewModel>();

            return builder.Build();
        }
    }
}