using IDPBookApp.ViewModel;

namespace IDPBookApp.Pages;

public partial class VacunaDetailPage : ContentPage
{
	public VacunaDetailPage(VacunaDetailViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}