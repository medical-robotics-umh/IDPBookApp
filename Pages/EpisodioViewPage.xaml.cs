using IDPBookApp.ViewModel;

namespace IDPBookApp.Pages;

public partial class EpisodioViewPage : ContentPage
{
	public EpisodioViewPage(DetailPageViewModel viewModel)
	{
		InitializeComponent(); 
		BindingContext = viewModel;        
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }    
}