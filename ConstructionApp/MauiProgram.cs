using Camera.MAUI;
using ConstructionApp.DataServices.Implementation;
using ConstructionApp.DataServices.Interface;
using ConstructionApp.Pages;
using ConstructionApp.Services.Implementation;
using ConstructionApp.Services.Interface;
using Microsoft.Extensions.Logging;

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

        builder.Services.AddSingleton<ILoginDataService, LoginDataService>();
        builder.Services.AddSingleton<IImageDataService, ImageDataService>();
        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddTransient<HomePage>();
        builder.Services.AddTransient<SavePhoto>();
        return builder.Build();
	}
}
