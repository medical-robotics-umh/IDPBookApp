using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;

namespace IDPBookApp.ViewModel;

[QueryProperty(nameof(Auth), nameof(Auth))]
public partial class MainViewModel : BaseViewModel
{
    [ObservableProperty]
    bool authAux;

    [ObservableProperty]
    string nPac;

    readonly FirebaseConnecty firebaseConnecty;
    public MainViewModel(FirebaseConnecty firebaseConnecty)
    {
        this.firebaseConnecty = firebaseConnecty;
        Auth = firebaseConnecty.userInfo.IsEmailVerified;
        AuthAux = !Auth;
        NPac = firebaseConnecty.userInfo.DisplayName;
    }

    [RelayCommand]
    async Task LogOutBtn()
    {
        bool ans = await App.Current.MainPage.DisplayAlert("Aviso.","¿Desea cerrar sesión?","Aceptar","Cancelar");
        if (ans==true)
        {
            firebaseConnecty.LogOut();
            //await App.Current.MainPage.DisplayAlert("Aviso", "Sesión finalizada correctamente", "Ok");
            await Shell.Current.GoToAsync("///LoginPage");
        }
    }
}
