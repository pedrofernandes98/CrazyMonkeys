using CrazyMonkeys.Services;
using CrazyMonkeys.View;
using CrazyMonkeys.ViewModel;

namespace CrazyMonkeys;

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
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddSingleton<MonkeyService>();

		builder.Services.AddSingleton<MonkeyViewModel>();
		//Cada vez que for necessário esta classe, será gerada uma nova instânica
		builder.Services.AddTransient<MonkeyDetailsViewModel>();

        builder.Services.AddSingleton<MainPage>();
		builder.Services.AddTransient<MonkeyDetailsPage>();

		//Plataforms Register
		builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
		builder.Services.AddSingleton<IGeolocation>(Geolocation.Default);
		builder.Services.AddSingleton<IMap>(Map.Default);

        return builder.Build();
	}
}
