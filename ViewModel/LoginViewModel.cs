using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;
using IDPBookApp.Models;
using IDPBookApp.Pages;

namespace IDPBookApp.ViewModel;
public partial class LoginViewModel : BaseViewModel
{
    readonly FirebaseConnecty firebaseConnecty;
    public LoginViewModel(FirebaseConnecty firebaseConnecty)
    {
        this.firebaseConnecty = firebaseConnecty;
        Chequear();
    }

    [ObservableProperty]
    string userName;

    [ObservableProperty]
    string userPassword;

    [ObservableProperty]
    string newPassword;

    [ObservableProperty]
    string verfPassword;

    public Cuestionario CuestInfo { get; set; } = new();

    [RelayCommand]
    async Task LoginBtn()
    {
        try
        {
            await firebaseConnecty.Login(UserName, UserPassword);
            await App.Current.MainPage.DisplayAlert("Bienvenido(a).", "Sesión iniciada correctamente", "Ok");
            Auth = firebaseConnecty.firebaseUserCredential.User.Info.IsEmailVerified;
            UserName = string.Empty; UserPassword = string.Empty;
            if (Auth == false)
            {
                var cuestInfo = await FirebaseConnecty.GetCuestionariosModel(firebaseConnecty.userInfo.Uid);
                await Shell.Current.GoToAsync($"{nameof(MainPage)}?Auth={Auth}");
                if (cuestInfo.Any())
                {
                    var lastCuest = cuestInfo.Last();
                    var fechaUltimoCuestionario = DateTime.Parse(lastCuest.QFecha);
                    var dif = DateTime.Today.Subtract(fechaUltimoCuestionario);
                    if (dif.Days > 183)
                    {                        
                        await App.Current.MainPage.DisplayAlert("Cuestinonario calidad de vida", "► Han pasado 6 meses desde la última vez que has realizado el cuestionario.\n► Antes de utilizar IDPBook, debes realizar uno nuevo.", "Ok");
                        await Shell.Current.GoToAsync($"{nameof(NuevaEncuestaPage)}", true,
                            new Dictionary<string, object>
                            {
                                {"Contador",cuestInfo.Count},
                                {"ValidCuest",false}
                            });
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Cuestinonario calidad de vida", "► Antes de empezar a utilizar IDPBook App, debes realizar el cuestionario de calidad de vida.", "Ok");
                    await Shell.Current.GoToAsync($"{nameof(NuevaEncuestaPage)}", true,
                        new Dictionary<string, object>
                        {
                            {"Contador",cuestInfo.Count},
                            {"ValidCuest",false}
                        });
                }
            }
            else
            {
                await Shell.Current.GoToAsync($"{nameof(MainPage)}?Auth={Auth}");

            }
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("INVALID_LOGIN_CREDENTIALS"))
            {
                await App.Current.MainPage.DisplayAlert("Credenciales invalidas", "Posibles causas:\n\n► La contraseña es incorrecta.\n► El correo no esta registrado en IDPBook.", "Ok");
            }
            if (ex.Message.Contains("INVALID_EMAIL"))
            {
                await App.Current.MainPage.DisplayAlert("Correo invalido", "Revisa que el correo no contenga espacios en blanco o cualquier caracter especial no permitido.", "Ok");
            }
            if (ex.Message.Contains("MISSING_EMAIL"))
            {
                await App.Current.MainPage.DisplayAlert("No hay correo", "Escribe un correo registrado en IDPBook.", "Ok");
            }
            if (ex.Message.Contains("MISSING_PASSWORD"))
            {
                await App.Current.MainPage.DisplayAlert("No hay contraseña", $"Escribe la contraseña asignada al correo:\n\n" + UserName, "Ok");
            }
        }
    }

    private async void Chequear()
    {
        var cond = await firebaseConnecty.CheckUser();
        if(cond == true)
        {
            Auth = firebaseConnecty.userInfo.IsEmailVerified;
            if (Auth == false)
            {
                var cuestInfo = await FirebaseConnecty.GetCuestionariosModel(firebaseConnecty.userInfo.Uid);                
                if (cuestInfo.Any())
                {
                    var lastCuest = cuestInfo.Last();
                    var fechaUltimoCuestionario = DateTime.Parse(lastCuest.QFecha);
                    var dif = DateTime.Today.Subtract(fechaUltimoCuestionario);
                    if (dif.Days > 183)
                    {
                        await App.Current.MainPage.DisplayAlert("Cuestinonario calidad de vida", "► Han pasado 6 meses desde la última vez que has realizado el cuestionario.\n► Antes de utilizar IDPBook, debes realizar uno nuevo.", "Ok");
                        await Shell.Current.GoToAsync($"{nameof(NuevaEncuestaPage)}", true,
                            new Dictionary<string, object>
                            {
                                {"Contador",cuestInfo.Count},
                                {"ValidCuest",false}
                            });
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Cuestinonario calidad de vida", "Antes de empezar a utilizar IDPBook App, debes realizar el cuestionario de calidad de vida.", "Ok");
                    await Shell.Current.GoToAsync($"{nameof(NuevaEncuestaPage)}", true,
                        new Dictionary<string, object>
                        {
                            {"Contador",cuestInfo.Count},
                            {"ValidCuest",false}
                        });
                }
            }
        }
    }

    [RelayCommand]
    Task ChangePassBtn() => Shell.Current.GoToAsync(nameof(CambiarPass));
}

