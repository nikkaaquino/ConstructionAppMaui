using Camera.MAUI;
using ConstructionApp.DataServices.Implementation;
using ConstructionApp.DataServices.Interface;
using ConstructionApp.Pages;
using ConstructionApp.Services.Implementation;
using ConstructionApp.Services.Interface;
using ConstructionApp.ViewModel;

namespace ConstructionApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCameraView()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
        builder.Services.AddSingleton<IGeolocation>(Geolocation.Default);
        builder.Services.AddSingleton<IMap>(Map.Default);

        builder.Services.AddSingleton<ILoginDataService, LoginDataService>();
        builder.Services.AddSingleton<PhotoDataService>();
        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddSingleton<HomePage>();
        builder.Services.AddSingleton<PhotoListPage>();
        builder.Services.AddSingleton<PhotoListPage>();
        builder.Services.AddSingleton<PhotoListViewModel>();

        builder.Services.AddSingleton<MonkeyService>();
        builder.Services.AddSingleton<MonkeysViewModel>();
        builder.Services.AddSingleton<MainPage>();

        builder.Services.AddTransient<DetailPage>();
        builder.Services.AddTransient<DetailViewModel>();

        return builder.Build();
    }
}
