using IDPBookApp.ViewModel;

namespace IDPBookApp.Pages;

public partial class EncuestaDetailPage : ContentPage
{
	public EncuestaDetailPage(EncuestaDetailViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}