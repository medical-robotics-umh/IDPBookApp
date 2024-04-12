using IDPBookApp.ViewModel;

namespace IDPBookApp.Pages;

public partial class NuevaEncuestaPage : ContentPage
{
	public NuevaEncuestaPage(NuevaEncuestaViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}