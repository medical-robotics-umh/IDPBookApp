using IDPBookApp.DataBase;
using IDPBookApp.Pages;
using IDPBookApp.ViewModel;
using Microsoft.Extensions.Logging;

namespace IDPBookApp;

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

#if DEBUG
		builder.Logging.AddDebug();

		builder.Services.AddSingleton<BaseViewModel>();
		builder.Services.AddSingleton<MainViewModel>();
		builder.Services.AddTransient<DetailPageViewModel>();
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<DetailPage>();
		builder.Services.AddSingleton<FirebaseConnecty>();
        builder.Services.AddSingleton<Estado>();
        builder.Services.AddSingleton<TipoLista>();
        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddSingleton<CitasPage>();
        builder.Services.AddTransient<Registro>();
        builder.Services.AddTransient<EpisodiosPage>();
        builder.Services.AddTransient<NEpisPage>();
        builder.Services.AddTransient<ListViewModel>();
        builder.Services.AddTransient<NewDataViewModel>();
        builder.Services.AddTransient<EpisodioViewPage>();

#endif
        return builder.Build();
	}
}
