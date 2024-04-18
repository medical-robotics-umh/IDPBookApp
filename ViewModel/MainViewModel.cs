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
            await Launcher.Default.OpenAsync("https://calendar.google.com/");
        }
        catch (Exception ex)
        {
            await App.Current.MainPage.DisplayAlert("Error", $"Se produjo un error: {ex.Message}", "Aceptar");
        }
    }

    [RelayCommand]
    async Task OpenEventCalendar()
    {
        try
        {
            //Abre un nuevo evento en Google calendar
            await Launcher.Default.OpenAsync("https://calendar.google.com/calendar/event?action=TEMPLATE");
        }
        catch (Exception ex)
        {
            await App.Current.MainPage.DisplayAlert("Error", $"Se produjo un error: {ex.Message}", "Aceptar");
        }
    }

    [RelayCommand]
    async Task OpenAgenda()
    {
        try
        {
            await Launcher.Default.OpenAsync("https://calendar.app.google/sMucecsooVgmy1oa9");
        }
        catch (Exception ex)
        {
            await App.Current.MainPage.DisplayAlert("Error", $"Se produjo un error: {ex.Message}", "Aceptar");
        }
    }

    [RelayCommand]
    async Task OpenChat()
    {
        try
        {
            await Launcher.Default.OpenAsync("https://chat.google.com/room/AAAA9tiUBzs?cls=7");
        }
        catch (Exception ex)
        {
            await App.Current.MainPage.DisplayAlert("Error", $"Se produjo un error: {ex.Message}", "Aceptar");
        }
    }

    [RelayCommand]
    async Task OpenNewGmailEmailAsync()
    {
        try
        {
            await Launcher.TryOpenAsync("mailto:?to=idpbook1@gmail.com &subject=Asunto del correo &body=Cuerpo del correo");
        }
        catch (Exception ex)
        {
            // Manejo de errores, por ejemplo, mostrar un mensaje de error al usuario
            Console.WriteLine($"No se pudo abrir Gmail para enviar el correo: {ex.Message}");
        }
    }

    [RelayCommand]
    async Task OpenPhoneDialer()
    {
        try
        {
            await Launcher.TryOpenAsync("tel:+123456789");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"No se pudo abrir la aplicación de teléfono: {ex.Message}");
        }
    }
}