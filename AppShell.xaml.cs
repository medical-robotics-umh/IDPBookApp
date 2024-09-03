using IDPBookApp.Pages;
using IDPBookApp.ViewModel;

namespace IDPBookApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(CambiarPass), typeof(CambiarPass));
        Routing.RegisterRoute(nameof(Estado), typeof(Estado));
        Routing.RegisterRoute(nameof(Registro), typeof(Registro));
        Routing.RegisterRoute(nameof(CitasPage), typeof(CitasPage));
        Routing.RegisterRoute(nameof(DocsPage), typeof(DocsPage));
        Routing.RegisterRoute(nameof(RedPage), typeof(RedPage));
        Routing.RegisterRoute(nameof(ContactoPage), typeof(ContactoPage));
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(EpisodiosPage), typeof(EpisodiosPage));
        Routing.RegisterRoute(nameof(NEpisPage), typeof(NEpisPage));
        Routing.RegisterRoute(nameof(EpisodioViewPage), typeof(EpisodioViewPage));
        Routing.RegisterRoute(nameof(ListaPacientesPage), typeof(ListaPacientesPage));
        Routing.RegisterRoute(nameof(DatosPacPage), typeof(DatosPacPage));
        Routing.RegisterRoute(nameof(HistorialPage), typeof(HistorialPage));
        Routing.RegisterRoute(nameof(NHistorial), typeof(NHistorial));
        Routing.RegisterRoute(nameof(HistoDetailPage), typeof(HistoDetailPage));
        Routing.RegisterRoute(nameof(PruebasLabPage), typeof(PruebasLabPage));
        Routing.RegisterRoute(nameof(NuevaAnalitica), typeof(NuevaAnalitica));
        Routing.RegisterRoute(nameof(AnltcDetailPage), typeof(AnltcDetailPage));
        Routing.RegisterRoute(nameof(TratPage), typeof(TratPage));
        Routing.RegisterRoute(nameof(NewTratPage), typeof(NewTratPage));
        Routing.RegisterRoute(nameof(OtroTratPage), typeof(OtroTratPage));
        Routing.RegisterRoute(nameof(NewOTratPage), typeof(NewOTratPage));
        Routing.RegisterRoute(nameof(OtroTratDetailPage), typeof(OtroTratDetailPage));
        Routing.RegisterRoute(nameof(VacunasPage), typeof(VacunasPage));
        Routing.RegisterRoute(nameof(Nueva_Vacuna), typeof(Nueva_Vacuna));
        Routing.RegisterRoute(nameof(VacunaDetailPage), typeof(VacunaDetailPage));
        Routing.RegisterRoute(nameof(EncuestasPage), typeof(EncuestasPage));
        Routing.RegisterRoute(nameof(EncuestaDetailPage), typeof(EncuestaDetailPage));
        Routing.RegisterRoute(nameof(NuevaEncuestaPage), typeof(NuevaEncuestaPage));
    }
}
