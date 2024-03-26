using IDPBookApp.ViewModel;

namespace IDPBookApp.Pages;

public partial class NewOTratPage : ContentPage
{
	public NewOTratPage(NewOTratViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}