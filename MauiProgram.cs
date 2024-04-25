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
		builder.Services.AddSingleton<FirebaseConnecty>();
        builder.Services.AddSingleton<Estado>();
        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddSingleton<CitasPage>();
        builder.Services.AddSingleton<ContactoPage>();
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
        builder.Services.AddTransient<HistoDetailPage>();
        builder.Services.AddTransient<HistoDetailViewModel>();
        builder.Services.AddTransient<PruebasLabPage>();
        builder.Services.AddTransient<PrbsLabViewModel>();
        builder.Services.AddTransient<NuevaAnalitica>();
        builder.Services.AddTransient<NewAnltcViewModel>();
        builder.Services.AddTransient<AnltcDetailPage>();
        builder.Services.AddTransient<AnltcDetailViewModel>();
        builder.Services.AddTransient<TratPage>();
        builder.Services.AddTransient<TratViewModel>();
        builder.Services.AddTransient<NewTratPage>();
        builder.Services.AddTransient<NewTratViewModel>();
        builder.Services.AddTransient<OtroTratPage>();
        builder.Services.AddTransient<NewOTratPage>();
        builder.Services.AddTransient<OtroTratDetailPage>();
        builder.Services.AddTransient<OtroTratViewModel>();
        builder.Services.AddTransient<NewOTratViewModel>();
        builder.Services.AddTransient<OTratDetailViewModel>();
        builder.Services.AddTransient<VacunasPage>();
        builder.Services.AddTransient<Nueva_Vacuna>();
        builder.Services.AddTransient<VacunaDetailPage>();
        builder.Services.AddTransient<VacunasViewModel>();
        builder.Services.AddTransient<NewVacunaViewModel>();
        builder.Services.AddTransient<VacunaDetailViewModel>();
        builder.Services.AddTransient<EncuestasPage>();
        builder.Services.AddTransient<EncuestaDetailPage>();
        builder.Services.AddTransient<NuevaEncuestaPage>();
        builder.Services.AddTransient<EncuestasViewModel>();
        builder.Services.AddTransient<EncuestaDetailViewModel>();
        builder.Services.AddTransient<NuevaEncuestaViewModel>();
        builder.Services.AddTransient<CambiarPass>();
        builder.Services.AddTransient<CamPassViewModel>();
#endif
        return builder.Build();
	}
}
