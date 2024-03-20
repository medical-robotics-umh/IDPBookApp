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
            await App.Current.MainPage.DisplayAlert("Bienvenid@.", "Sesión iniciada correctamente", "Ok");
            Auth = firebaseConnecty.firebaseUserCredential.User.Info.IsEmailVerified;
            await Shell.Current.GoToAsync($"{nameof(MainPage)}?Auth={Auth}");
            UserName = string.Empty; UserPassword = string.Empty;
        }
        catch (Exception ex)
        {
            if(ex.Message.Contains("INVALID_LOGIN_CREDENTIALS"))
            {
                await App.Current.MainPage.DisplayAlert("Aviso.","Las credenciales propocionadas no coinciden con ningun usuario","Ok");
            }
            else if (ex.Message.Contains("INVALID_EMAIL"))
            {
                await App.Current.MainPage.DisplayAlert("Aviso.","Revisa que el correo no contenga espacios en blanco o cualquier caracter especial no permitido.", "Ok");
            }            
        }
    }

    [RelayCommand]
    //Aqui podria pasar el correo hacia la pag de cambiar pswd
    Task ChangePassBtn() => Shell.Current.GoToAsync(nameof(CambiarPass));
    
}

