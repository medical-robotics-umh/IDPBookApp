using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;

namespace IDPBookApp.ViewModel;

public partial class MainViewModel : BaseViewModel
{
    readonly FirebaseConnecty firebaseConnecty;
    public MainViewModel(FirebaseConnecty firebaseConnecty) 
    {
        this.firebaseConnecty = firebaseConnecty;
    }

    [RelayCommand]
    async Task LogOutBtn()
    {
        firebaseConnecty.LogOut();
        await App.Current.MainPage.DisplayAlert("Aviso", "Sesión finalizada correctamente", "Ok");
        await Shell.Current.GoToAsync("///LoginPage");
    }
}
