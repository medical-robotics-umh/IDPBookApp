using IDPBookApp.ViewModel;

namespace IDPBookApp.Pages;

public partial class PruebasLabPage : ContentPage
{
	public PruebasLabPage(PrbsLabViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}