using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;

namespace IDPBookApp.ViewModel;
public partial class LoginViewModel : ObservableObject
{
    FirebaseConnecty firebaseConnecty;

    public LoginViewModel(FirebaseConnecty firebaseConnecty)
    {
        this.firebaseConnecty = firebaseConnecty;
    }

    [ObservableProperty]
    string userName;

    [ObservableProperty]
    string userPassword;

    [RelayCommand]
    async Task LoginBtn()
    {
        try
        {
            var user = await firebaseConnecty.Login(UserName, UserPassword);
            await App.Current.MainPage.DisplayAlert("Bienvenido", "Sesión iniciada correctamente", "Ok");
        }
        catch (Exception ex)
        {
            if(ex.Message.Contains("INVALID_LOGIN_CREDENTIALS"))
            {
                await App.Current.MainPage.DisplayAlert("Alerta","INVALID_LOGIN_CREDENTIALS","Ok");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alerta",ex.Message, "Ok");
            }
            
        }
    }
}

