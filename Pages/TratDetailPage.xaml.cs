using IDPBookApp.ViewModel;

namespace IDPBookApp.Pages;

public partial class TratDetailPage : ContentPage
{
	public TratDetailPage(TratDetailViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}