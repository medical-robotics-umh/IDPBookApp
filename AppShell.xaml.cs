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
    }
}
