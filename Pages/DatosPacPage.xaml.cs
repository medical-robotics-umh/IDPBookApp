using IDPBookApp.ViewModel;

namespace IDPBookApp.Pages;

public partial class DatosPacPage : ContentPage
{
	public DatosPacPage(DatosPacViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;	
	}

}