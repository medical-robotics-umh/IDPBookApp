using IDPBookApp.Pages;
using IDPBookApp.Models;
using IDPBookApp.ViewModel;

namespace IDPBookApp;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
}
 