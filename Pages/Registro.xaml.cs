namespace IDPBookApp.Pages;
using IDPBookApp.ViewModel;

public partial class Registro : ContentPage
{
	public Registro(MainViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}