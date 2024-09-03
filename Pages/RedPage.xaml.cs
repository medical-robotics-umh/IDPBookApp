namespace IDPBookApp.Pages;
using IDPBookApp.ViewModel;

public partial class RedPage : ContentPage
{
	public RedPage(MainViewModel mainViewModel)
	{
		InitializeComponent();
		BindingContext = mainViewModel;
	}
}