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
    static async Task NavDrive()
    {
        try
        {
            await Launcher.Default.OpenAsync("https://drive.google.com/drive/folders/1SDSi4fPLPCWrm4bAsB9wNj-Ne-V-e6z5?usp=sharing");
        }
        catch
        {
            await App.Current.MainPage.DisplayAlert("Algo salio mal", $"Se ha producido un error en:\n MainViewModel -> Task NavDrive", "Aceptar");
        }
    }

    [RelayCommand]
    static async Task NavYoutube()
    {
        try
        {
            await Launcher.Default.OpenAsync("https://www.youtube.com/user/nuevohospitallafe1");
        }
        catch
        {
            await App.Current.MainPage.DisplayAlert("Algo salio mal", $"Se ha producido un error en:\n MainViewModel -> Task NavYoutube", "Aceptar");
        }
    }

    [RelayCommand]
    static async Task OpenCalendar()
    {
        try
        {
            await Launcher.Default.OpenAsync("https://calendar.google.com/");
        }
        catch
        {
            await App.Current.MainPage.DisplayAlert("Algo salio mal", $"Se ha producido un error en:\n MainViewModel -> Task OpenCalendar", "Aceptar");
        }
    }

    [RelayCommand]
    static async Task OpenEventCalendar()
    {
        try
        {
            await Launcher.Default.OpenAsync("https://calendar.google.com/calendar/event?action=TEMPLATE");
        }
        catch
        {
            await App.Current.MainPage.DisplayAlert("Algo salio mal", $"Se ha producido un error en:\n MainViewModel -> Task OpenEventCalendar", "Aceptar");
        }
    }

    [RelayCommand]
    static async Task OpenAgenda()
    {
        try
        {
            await Launcher.Default.OpenAsync("https://calendar.app.google/V59SGBDpAcbBTr246");
        }
        catch
        {
            await App.Current.MainPage.DisplayAlert("Algo salio mal", $"Se ha producido un error en:\n MainViewModel -> Task OpenAgenda", "Aceptar");
        }
    }

    [RelayCommand]
    static async Task OpenChat()
    {
        try
        {
            await Launcher.Default.OpenAsync("https://chat.google.com/room/AAAA9tiUBzs?cls=7");
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

    [RelayCommand]
    static async Task OpenMeet()
    {
        try
        {
            await Launcher.TryOpenAsync("https://meet.google.com/landing");
        }
        catch
        {
            await App.Current.MainPage.DisplayAlert("Algo salio mal", $"Se ha producido un error en:\n MainViewModel -> Task OpenPhoneDialer", "Aceptar");
        }
    }
}