using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;
using IDPBookApp.Models;
using IDPBookApp.Pages;

namespace IDPBookApp.ViewModel;

[QueryProperty(nameof(Auth), nameof(Auth))]
public partial class MainViewModel : BaseViewModel
{
    readonly FirebaseConnecty firebaseConnecty;
    public MainViewModel(FirebaseConnecty firebaseConnecty)
    {
        this.firebaseConnecty = firebaseConnecty;
        Auth = firebaseConnecty.userInfo.IsEmailVerified;
        NPac = firebaseConnecty.pacInfo.DisplayName;
        NMed = firebaseConnecty.userInfo.DisplayName;
    }

    [RelayCommand]
    async Task LogOutBtn()
    {
        bool ans = await App.Current.MainPage.DisplayAlert("Aviso.", "¿Desea cerrar sesión?", "Aceptar", "Cancelar");
        if (ans == true)
        {
            firebaseConnecty.LogOut();
            //await App.Current.MainPage.DisplayAlert("Aviso", "Sesión finalizada correctamente", "Ok");
            await Shell.Current.GoToAsync("///LoginPage");
        }
    }

    [RelayCommand]
    static async Task NavDrive()
    {
        await Launcher.Default.OpenAsync("https://drive.google.com/drive/folders/1VNLqw9L-HRp4MsbtxdyZEGqcCsuWG4Td?usp=sharing");
    }

    [RelayCommand]
    async Task OpenCalendar()
    {
        try
        {
            await Launcher.OpenAsync("Calendario:");
        }
        catch (Exception ex)
        {
            await App.Current.MainPage.DisplayAlert("Error", $"Se produjo un error: {ex.Message}", "Aceptar");
        }
    }
}
