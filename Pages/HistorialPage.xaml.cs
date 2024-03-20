using IDPBookApp.ViewModel;

namespace IDPBookApp.Pages;

public partial class HistorialPage : ContentPage
{
	public HistorialPage(HistoViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}