using IDPBookApp.ViewModel;
namespace IDPBookApp.Pages;

public partial class AdminDetailPage : ContentPage
{
	public AdminDetailPage(AdminDetailViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}