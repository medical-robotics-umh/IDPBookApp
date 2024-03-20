using CommunityToolkit.Maui.Core.Platform;
using IDPBookApp.ViewModel;
namespace IDPBookApp.Pages;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginViewModel loginViewModel)
	{
		InitializeComponent();
		BindingContext = loginViewModel;
	}

    private async void Button_Pressed(object sender, EventArgs e)
    {
        await pas.HideKeyboardAsync(CancellationToken.None);
    }
}