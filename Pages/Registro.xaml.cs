namespace IDPBookApp.Pages;
using IDPBookApp.ViewModel;

public partial class Registro : ContentPage
{
	public Registro(NewDataViewModel newDataViewModel)
	{
		InitializeComponent();
		BindingContext = newDataViewModel;
    }
}