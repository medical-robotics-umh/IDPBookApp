namespace IDPBookApp.Pages;
using IDPBookApp.ViewModel;

public partial class Registro : ContentPage
{
	public Registro(NewPacViewModel newPac)
	{
		InitializeComponent();
		BindingContext = newPac;
    }
}