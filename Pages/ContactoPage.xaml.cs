using IDPBookApp.ViewModel;

namespace IDPBookApp.Pages;

public partial class ContactoPage : ContentPage
{
	public ContactoPage(MainViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}