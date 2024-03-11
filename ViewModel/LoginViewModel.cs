using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;
using IDPBookApp.Pages;

namespace IDPBookApp.ViewModel;
public partial class LoginViewModel : BaseViewModel
{
    readonly FirebaseConnecty firebaseConnecty;
    public LoginViewModel(FirebaseConnecty firebaseConnecty)
    {
        this.firebaseConnecty = firebaseConnecty;
        firebaseConnecty.CheckUser();
    }

    [ObservableProperty]
    string userName;

    [ObservableProperty]
    string userPassword;

    [ObservableProperty]
    string newuserPassword;

    [RelayCommand]
    async Task LoginBtn()
    {
        try
        {
            await firebaseConnecty.Login(UserName, UserPassword);
            await App.Current.MainPage.DisplayAlert("Bienvenid@.", "Sesión iniciada correctamente "+firebaseConnecty.userInfo.IsEmailVerified.ToString(), "Ok");
            Auth = firebaseConnecty.userInfo.IsEmailVerified;
            await Shell.Current.GoToAsync($"{nameof(MainPage)}?Auth={Auth}");
            UserName = string.Empty; UserPassword = string.Empty;
        }
        catch (Exception ex)
        {
            if(ex.Message.Contains("INVALID_LOGIN_CREDENTIALS"))
            {
                await App.Current.MainPage.DisplayAlert("Aviso.","INVALID_LOGIN_CREDENTIALS","Ok");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Aviso.",ex.Message, "Ok");
            }            
        }
    }

    [RelayCommand]
    //Aqui podria pasar el correo hacia la pag de cambiar pswd
    Task ChangePassBtn() => Shell.Current.GoToAsync(nameof(CambiarPass));
    
}

