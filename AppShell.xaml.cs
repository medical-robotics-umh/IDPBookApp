using IDPBookApp.Pages;

namespace IDPBookApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(CambiarPass), typeof(CambiarPass));
        Routing.RegisterRoute(nameof(DetailPage), typeof(DetailPage));
        Routing.RegisterRoute(nameof(Estado), typeof(Estado));
        Routing.RegisterRoute(nameof(Registro), typeof(Registro));
        Routing.RegisterRoute(nameof(CitasPage), typeof(CitasPage));
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(EpisodiosPage), typeof(EpisodiosPage));
        Routing.RegisterRoute(nameof(NEpisPage), typeof(NEpisPage));
        Routing.RegisterRoute(nameof(EpisodioViewPage), typeof(EpisodioViewPage));
        Routing.RegisterRoute(nameof(ListaPacientesPage), typeof(ListaPacientesPage));
        Routing.RegisterRoute(nameof(DatosPacPage), typeof(DatosPacPage));
        Routing.RegisterRoute(nameof(HistorialPage), typeof(HistorialPage));
        Routing.RegisterRoute(nameof(NHistorial), typeof(NHistorial));
    }
}
