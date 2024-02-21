using IDPBookApp.ViewModel;
namespace IDPBookApp.Pages;

public partial class NEpisPage : ContentPage
{
	public NEpisPage(NewDataViewModel newDataViewModel)
	{
		InitializeComponent();
		BindingContext = newDataViewModel;
	}
}