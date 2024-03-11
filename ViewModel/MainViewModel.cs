using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;

namespace IDPBookApp.ViewModel;

[QueryProperty(nameof(Auth), nameof(Auth))]
public partial class MainViewModel : BaseViewModel
{
    [ObservableProperty]
    bool authAux;

    readonly FirebaseConnecty firebaseConnecty;
    public MainViewModel(FirebaseConnecty firebaseConnecty)
    {
        this.firebaseConnecty = firebaseConnecty;
        Auth = firebaseConnecty.userInfo.IsEmailVerified;
        AuthAux = !Auth;
    }

    [RelayCommand]
    async Task LogOutBtn()
    {
        firebaseConnecty.LogOut();
        await App.Current.MainPage.DisplayAlert("Aviso", "Sesión finalizada correctamente", "Ok");
        await Shell.Current.GoToAsync("///LoginPage");
    }
}
