using IDPBookApp.ViewModel;

namespace IDPBookApp.Pages;

public partial class EncuestasPage : ContentPage
{
	public EncuestasPage(EncuestasViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}