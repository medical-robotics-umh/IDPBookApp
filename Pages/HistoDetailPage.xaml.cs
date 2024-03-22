using IDPBookApp.ViewModel;

namespace IDPBookApp.Pages;

public partial class HistoDetailPage : ContentPage
{
	public HistoDetailPage(HistoDetailViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
	}
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}