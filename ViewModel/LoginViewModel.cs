using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;
using IDPBookApp.Pages;

namespace IDPBookApp.ViewModel;
public partial class LoginViewModel : ObservableObject
{
    FirebaseConnecty firebaseConnecty;
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
            await Shell.Current.GoToAsync(nameof(MainPage));
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
    Task ChangePassBtn() => Shell.Current.GoToAsync(nameof(CambiarPass));
    
}

