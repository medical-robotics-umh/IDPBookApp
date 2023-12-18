using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;
using IDPBookApp.Pages;

namespace IDPBookApp.ViewModel;
public partial class LoginViewModel : ObservableObject
{
    FirebaseConnecty firebaseConnecty;
    private INavigation navigation;
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
   void LogOutBtn()
    { 
        firebaseConnecty.LogOut();
        App.Current.MainPage.DisplayAlert("Aviso", "Sesión finalizada correctamente", "Ok");
    }

    [RelayCommand]
    Task ChangePassBtn() => Shell.Current.GoToAsync(nameof(MainPage));
    
}

