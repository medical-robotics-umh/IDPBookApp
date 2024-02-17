using IDPBookApp.ViewModel;
namespace IDPBookApp.Pages;

public partial class EpisodiosPage : ContentPage
{
	public EpisodiosPage(MainViewModel mainViewModel)
	{
		InitializeComponent();
		BindingContext = mainViewModel;
    }
}