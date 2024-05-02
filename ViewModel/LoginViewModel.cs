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
    string newPassword;

    [ObservableProperty]
    string verfPassword;
    
    [RelayCommand]
    async Task LoginBtn()
    {
        try
        {
            await firebaseConnecty.Login(UserName, UserPassword);
            await App.Current.MainPage.DisplayAlert("Bienvenid@.", "Sesión iniciada correctamente", "Ok");
            Auth = firebaseConnecty.firebaseUserCredential.User.Info.IsEmailVerified;
            UserName = string.Empty; UserPassword = string.Empty;
            await Shell.Current.GoToAsync($"{nameof(MainPage)}?Auth={Auth}");
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("INVALID_LOGIN_CREDENTIALS"))
            {
                await App.Current.MainPage.DisplayAlert("Aviso.", "Las credenciales propocionadas no coinciden con ningun usuario.", "Ok");
            }
            if (ex.Message.Contains("INVALID_EMAIL"))
            {
                await App.Current.MainPage.DisplayAlert("Aviso.", "Revisa que el correo no contenga espacios en blanco o cualquier caracter especial no permitido.", "Ok");
            }
            await App.Current.MainPage.DisplayAlert("Algo salio mal", $"Se ha producido un error en:\n LoginViewModel", "Aceptar");
        }
    }

    [RelayCommand]
    Task ChangePassBtn() => Shell.Current.GoToAsync(nameof(CambiarPass));    
}

