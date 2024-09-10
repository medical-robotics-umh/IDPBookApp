using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;

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
    async Task GoToNav(string ruta)
    {
        if (NPac != null)
        {
            await Shell.Current.GoToAsync(ruta);
        }
        else
        {
            await Shell.Current.DisplayAlert("¡Aviso!", "Elije o crea un paciente para acceder a estos apartados.", "Ok");
        }
    }

    [RelayCommand]
    async Task LogOutBtn()
    {
        bool ans = await App.Current.MainPage.DisplayAlert("Saliendo de la aplicación", "¿Desea cerrar sesión?", "Aceptar", "Cancelar");
        if (ans == true)
        {
            firebaseConnecty.LogOut();
            await Shell.Current.GoToAsync("///LoginPage");
        }
    }

    [RelayCommand]
    static async Task NavWeb(string url)
    {
        try
        {
            await Launcher.Default.OpenAsync(url);
        }
        catch
        {
            await App.Current.MainPage.DisplayAlert("Algo salio mal", $"Se ha producido un error en:\n MainViewModel -> Task OpenChat", "Aceptar");
        }
    }

    [RelayCommand]
    static async Task OpenNewGmailEmailAsync()
    {
        try
        {
            await Launcher.TryOpenAsync("mailto:?to=idpbook1@gmail.com &subject=Contacto sanitario IDPBook App");
        }
        catch
        {
            await App.Current.MainPage.DisplayAlert("Algo salio mal", $"Se ha producido un error en:\n MainViewModel -> Task OpenNewGmail", "Aceptar");
        }
    }
}