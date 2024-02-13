namespace IDPBookApp.Pages;
using IDPBookApp.ViewModel;

public partial class CitasPage : ContentPage
{
	public CitasPage(MainViewModel mainViewModel)
	{
		InitializeComponent();
		BindingContext = mainViewModel;
    }
}