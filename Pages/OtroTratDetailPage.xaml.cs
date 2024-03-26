using IDPBookApp.ViewModel;

namespace IDPBookApp.Pages;

public partial class OtroTratDetailPage : ContentPage
{
	public OtroTratDetailPage(OTratDetailViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}