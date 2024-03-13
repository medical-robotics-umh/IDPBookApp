namespace IDPBookApp.Pages;
using IDPBookApp.ViewModel;

public partial class ListaPacientesPage : ContentPage
{
	public ListaPacientesPage(ListaPacViewModel listaPac)
	{
        //InitializeComponent();
		BindingContext = listaPac;
	}
}