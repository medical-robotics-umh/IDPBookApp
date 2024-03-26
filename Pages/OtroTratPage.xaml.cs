using IDPBookApp.ViewModel;

namespace IDPBookApp.Pages;

public partial class OtroTratPage : ContentPage
{
	public OtroTratPage(OtroTratViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}