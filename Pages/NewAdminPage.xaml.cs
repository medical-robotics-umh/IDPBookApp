using IDPBookApp.ViewModel;
namespace IDPBookApp.Pages;

public partial class NewAdminPage : ContentPage
{
	public NewAdminPage(NewAdminViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}