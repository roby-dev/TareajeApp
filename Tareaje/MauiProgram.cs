using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.WebView.Maui;
using Tareaje.Data;
using Tareaje.Interfaces;
using Tareaje.Services;

namespace Tareaje;

public static class MauiProgram {
	private static readonly string backendURL = @"https://29b9-187-86-166-195.sa.ngrok.io/";
	//private static readonly string backendURL = @"http://localhost:5214/";
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "Open" +
					"SansRegular");
			});

		builder.Services.AddMauiBlazorWebView();
		#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
#endif
        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(backendURL) });
        builder.Services.AddSingleton<WeatherForecastService>();
		builder.Services.AddSingleton<IOfficeService,OfficeService>();
		builder.Services.AddSingleton<IAuthService,AuthService>();
        builder.Services.AddTelerikBlazor();
        builder.Services.AddSweetAlert2();

        return builder.Build();
	}	
}
