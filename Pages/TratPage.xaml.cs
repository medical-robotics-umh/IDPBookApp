using IDPBookApp.ViewModel;

namespace IDPBookApp.Pages;

public partial class TratPage : ContentPage
{
	public TratPage(TratViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}