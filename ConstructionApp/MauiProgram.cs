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
        builder.Services.AddSingleton<IPhotoDataService, PhotoDataService>();
        builder.Services.AddSingleton<PhotoDataService>();
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddSingleton<HomePage>();
        builder.Services.AddSingleton<HomeViewModel>();
        builder.Services.AddSingleton<LoginViewModel>();

        return builder.Build();
    }
}
