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
            if (url == "llamada")
            {
                TimeSpan currentTime = DateTime.Now.TimeOfDay;
                TimeSpan startTime = new(8, 30, 0);  // 8:30 AM
                TimeSpan endTime = new(15, 0, 0);    // 3:00 PM
                if (currentTime >= startTime && currentTime <= endTime)
                {
                    //var now = DateTime.UtcNow;
                    //var start = now.AddMinutes(10).ToString("yyyyMMddTHHmm00Z");
                    //var end = now.AddMinutes(30).ToString("yyyyMMddTHHmm00Z");
                    //await Shell.Current.DisplayAlert("Llamada solicitada", "El calendario se abrirá para confirmar la solicitud. Solo presiona 'Guardar' para confirmar.\n\nLa Unidad te llamara en unos minutos.", "Ok");
                    //await Launcher.Default.OpenAsync("https://www.google.com/calendar/render?action=TEMPLATE&text=Llamada Unidad Inmuno. Primarias&dates=" + start + "/" + end + "&add=idpbook1@gmail.com");
                    await Shell.Current.DisplayAlert("Iniciando llamada...", "A continuación, se abrirá Google Meet para iniciar la video llamada con la Unidad.", "Ok");
                    await Launcher.Default.OpenAsync("https://duo.app.goo.gl/wDajLjCnSfarP2rY9OCG8F");
                }
                else
                {
                    await Shell.Current.DisplayAlert("No disponible", "El horario de llamadas es de 8:30 a 15:00.\nIntentalo nuevamente mañana.", "Ok");
                }
            }
            else
            {
                await Launcher.Default.OpenAsync(url);
            }
        }
        catch
        {
            await App.Current.MainPage.DisplayAlert("Algo salio mal", $"Se ha producido un error en:\n MainViewModel -> Task NavWeb", "Aceptar");
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