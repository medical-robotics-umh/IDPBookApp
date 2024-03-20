using IDPBookApp.DataBase;
using IDPBookApp.Pages;
using IDPBookApp.ViewModel;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;

namespace IDPBookApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();

		builder.Services.AddSingleton<BaseViewModel>();
		builder.Services.AddTransient<MainViewModel>();
		builder.Services.AddTransient<DetailPageViewModel>();
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<MainPage>();
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
        builder.Services.AddTransient<ListaPacientesPage>();
        builder.Services.AddTransient<ListaPacViewModel>();
        builder.Services.AddTransient<NewPacViewModel>();
        builder.Services.AddTransient<DatosPacPage>();
        builder.Services.AddTransient<DatosPacViewModel>();
        builder.Services.AddTransient<HistorialPage>();
        builder.Services.AddTransient<HistoViewModel>();
        builder.Services.AddTransient<NHistorial>();
        builder.Services.AddTransient<NewHistoViewModel>();

#endif
        return builder.Build();
	}
}
