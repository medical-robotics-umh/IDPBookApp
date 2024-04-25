using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;
using IDPBookApp.Models;
using IDPBookApp.Pages;
using Plugin.CloudFirestore;

namespace IDPBookApp.ViewModel;

public partial class NewPacViewModel : BaseViewModel
{
    readonly FirebaseConnecty firebaseConnecty;
    private readonly ListaPacViewModel listaPacViewModel;
    static INavigation Navigation => Shell.Current.Navigation;
    public NewPacViewModel(FirebaseConnecty firebaseConnecty)
    {
        this.firebaseConnecty = firebaseConnecty;
        DiagSelec = new bool[ListaDiagcs.Count];
    }

    [ObservableProperty]
    private bool diagncsVisbl;

    [ObservableProperty]
    string nombPac = string.Empty;
    [ObservableProperty]
    string apllPac;
    [ObservableProperty]
    string emailPac;
    [ObservableProperty]
    int sexPac = -1;
    [ObservableProperty]
    DateTime fNac = DateTime.Today;
    [ObservableProperty]
    int edadPac;
    [ObservableProperty]
    int tratPac = -1;
    [ObservableProperty]
    bool[] diagSelec;
    [ObservableProperty]
    string otroDiag;
    [ObservableProperty]
    DateTime fDiag = DateTime.Today;

    [RelayCommand]
    private void VisibleDiag()
    {
        DiagncsVisbl = !DiagncsVisbl;
    }

    [RelayCommand]
    async Task NewUser()
    {
        if (NombPac != string.Empty)
        {
            if (Disable == true)
            {
                try
                {
                    await firebaseConnecty.RegistPac(EmailPac, "12345678", NombPac);
                    //TimeSpan Date = DateTimeOffset.Now.ToUnixTimeSeconds().ToString()
                    var NuevoPaciente = new Paciente
                    {
                        IdMed = firebaseConnecty.userInfo.Uid,
                        Nombre = NombPac,
                        Apelld = ApllPac,
                        Correo = EmailPac,
                        Sexo = SexPac,
                        FechNac = FNac.ToShortDateString(),
                        TratAct = TratPac,
                        Diagnsc = DiagSelec,
                        OtroDiag = OtroDiag,
                        FechDiag = FDiag.ToShortDateString(),
                        Pass = "12345678"
                    };
                    await CrossCloudFirestore.Current
                                    .Instance
                                    .Collection("IDPbookDB")
                                    .Document(firebaseConnecty.pacInfo.Uid)
                                    .SetAsync(NuevoPaciente);
                    await App.Current.MainPage.DisplayAlert("Correcto", "Se ha creado paciente: " + NombPac, "Ok");
                    NombPac = ApllPac = EmailPac = OtroDiag = string.Empty;
                    FNac = FDiag = DateTime.Today;
                    SexPac = -1;

                    var Pac = new ListaPacientesPage(listaPacViewModel)
                    {
                        BindingContext = new ListaPacViewModel(firebaseConnecty)
                    };
                    await Navigation.PopAsync();
                    Navigation.InsertPageBefore(Pac, Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
                    await Shell.Current.GoToAsync("..");
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("MISSING_EMAIL"))
                    {
                        await App.Current.MainPage.DisplayAlert("Aviso.", "Favor ingresa un correo para el usuario nuevo.", "Ok");
                    }
                    else if (ex.Message.Contains("INVALID_EMAIL"))
                    {
                        await App.Current.MainPage.DisplayAlert("Aviso.", "El correo proporcionado no es valido", "Ok");
                    }
                }                
            }
            else if (Disable == false)
            {
                try
                {
                    await firebaseConnecty.RegistMed(EmailPac, "0987654", NombPac);
                    await CrossCloudFirestore.Current
                                    .Instance
                                    .Collection("IDPbookDB")
                                    .Document(firebaseConnecty.userInfo.Uid)
                                    .SetAsync("Correo",EmailPac);
                    await App.Current.MainPage.DisplayAlert("Correcto", "Se ha creado personal médico: " + NombPac + ". Se ha enviado correo de autenticación al correo del usuario.", "Ok");
                    NombPac = ApllPac = EmailPac = string.Empty;
                    MedCheck = false;
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("MISSING_EMAIL"))
                    {
                        await App.Current.MainPage.DisplayAlert("Aviso.", "Favor ingresa un correo para el usuario nuevo.", "Ok");
                    }
                    else if (ex.Message.Contains("INVALID_EMAIL"))
                    {
                        await App.Current.MainPage.DisplayAlert("Aviso.", "El correo proporcionado no es valido", "Ok");
                    }
                }
            }
        }
        else
        {
            await App.Current.MainPage.DisplayAlert("Aviso.", "Favor ingresa el nombre del usuario nuevo.", "Ok");
        }
    }
}
