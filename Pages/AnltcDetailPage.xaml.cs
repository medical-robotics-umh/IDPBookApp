using IDPBookApp.ViewModel;

namespace IDPBookApp.Pages;

public partial class AnltcDetailPage : ContentPage
{
	public AnltcDetailPage(AnltcDetailViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}