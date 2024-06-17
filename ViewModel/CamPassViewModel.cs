using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;
using Plugin.CloudFirestore;

namespace IDPBookApp.ViewModel;

public partial class CamPassViewModel : BaseViewModel
{
    readonly FirebaseConnecty firebaseConnecty;
    public CamPassViewModel(FirebaseConnecty firebaseConnecty)
    {
        this.firebaseConnecty = firebaseConnecty;
        VPass = true;
    }

    [ObservableProperty]
    string userMail;

    [ObservableProperty]
    string newPassword;

    [ObservableProperty]
    string verfPassword;

    [ObservableProperty]
    public bool vPass;

    [RelayCommand]
    public async Task SendNewPass()
    {
        Run = true;
        if (MedCheck == true)
        {
            var query = await CrossCloudFirestore.Current
                 .Instance
                 .Collection("/IDPbookDB")
                 .WhereEqualsTo("Correo", UserMail)
                 .GetAsync();
            if (query.Count == 0)
            {
                await App.Current.MainPage.DisplayAlert("Usuario no encontrado", $"No existe un usuario registrado con el correo:\n\n" + UserMail + "\n\nComprueba el correo o comunicate con el personal médico.", "Ok");
            }
            else
            {
                await firebaseConnecty.SendEmailAsync(UserMail);
                await App.Current.MainPage.DisplayAlert("Solicitud correcta", "Se ha enviado un email con las instrucciones para restablecer tu contraseña, por favor revisa tu correo.", "Salir");
                UserMail = string.Empty;
            }
        }
        else
        {
            if (VerfPassword != string.Empty & VerfPassword != null)
            {
                if (VerfPassword.Length >= 6)
                {
                    if (NewPassword == VerfPassword)
                    {
                        try
                        {
                            await firebaseConnecty.ChangePasswordAsync(UserMail, VerfPassword);
                            var ban = await App.Current.MainPage.DisplayAlert("Solicitud correcta", "Se ha actualizado la contraseña para el usuario:\n\n" + UserMail, "Iniciar sesión", "Salir");
                            if (ban == true)
                            {
                                await Shell.Current.GoToAsync("..");
                            }
                            VerfPassword = NewPassword = UserMail = string.Empty;
                        }
                        catch (Exception ex)
                        {
                            if (ex.Message.Contains("Index"))
                            {
                                await App.Current.MainPage.DisplayAlert("Usuario no encontrado.", $"No existe un usuario registrado con el correo:\n\n" + UserMail + "\n\nComprueba el correo o comunicate con el personal médico.", "Ok");
                            }
                            if (ex.Message.Contains("MISSING_PASSWORD"))
                            {
                                await App.Current.MainPage.DisplayAlert("Usuario incorrecto.", $"Al parecer, el usuario:\n\n" + UserMail + "\n\nPertenece al personal médico, intenta de nuevo asegurandote de marcar la casilla Per. médico.", "Ok");
                            }
                        }
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Verificar contraseña", "Las contraseñas no coinciden.", "Ok");
                    }
                }
                else
                {
                    await Shell.Current.DisplayAlert("Contraseña no válida", "Las contraseña debe contener al menos 6 caracteres.", "Ok");
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Campos vacios", "Escribe la nueva contraseña en los dos campos dispponibles.", "Ok");
            }
        }
        Run = false;
    }
}
