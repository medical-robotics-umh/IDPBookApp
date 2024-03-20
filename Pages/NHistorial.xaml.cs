using IDPBookApp.ViewModel;

namespace IDPBookApp.Pages;

public partial class NHistorial : ContentPage
{
	public NHistorial(NewHistoViewModel newHisto)
	{
		InitializeComponent();
		BindingContext = newHisto;
	}
}