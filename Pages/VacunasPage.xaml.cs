using IDPBookApp.ViewModel;

namespace IDPBookApp.Pages;

public partial class VacunasPage : ContentPage
{
	public VacunasPage(VacunasViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}