using IDPBookApp.ViewModel;
namespace IDPBookApp.Pages;

public partial class DocsPage : ContentPage
{
	public DocsPage(MainViewModel mainViewModel)
	{
		InitializeComponent();
		BindingContext = mainViewModel;
	}
}