using IDPBookApp.ViewModel;
namespace IDPBookApp.Pages;

public partial class EpisodiosPage : ContentPage
{
	public EpisodiosPage(ListViewModel listViewModel)
	{
		InitializeComponent();
		BindingContext = listViewModel;
    }
}