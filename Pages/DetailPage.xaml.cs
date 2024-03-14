using IDPBookApp.ViewModel;

namespace IDPBookApp.Pages;

public partial class DetailPage : ContentPage
{
	public DetailPage(DetailPageViewModel detailPageViewModel)
	{
		InitializeComponent();
		BindingContext = detailPageViewModel;
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}