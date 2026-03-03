using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;
using IDPBookApp.Models;
using IDPBookApp.Pages;

namespace IDPBookApp.ViewModel;

public partial class NewPacViewModel : BaseViewModel
{
    readonly FirebaseConnecty firebaseConnecty;
    private readonly ListaPacViewModel listaPacViewModel;
    static INavigation Navigation => Shell.Current.Navigation;
    public NewPacViewModel(FirebaseConnecty firebaseConnecty)
    {
        this.firebaseConnecty = firebaseConnecty;
    }

    [ObservableProperty]
    string nombPac = string.Empty;
    [ObservableProperty]
    string apllPac;
    [ObservableProperty]
    string emailPac;
    [ObservableProperty]
    DateTime fNac = DateTime.Today;
    [ObservableProperty]
    int edadPac;
    [ObservableProperty]
    string otroDiag1;
    [ObservableProperty]
    string otroDiag2;
    [ObservableProperty]
    DateTime fDiag = DateTime.Today;
    [ObservableProperty]
    DateTime fDiag1 = DateTime.Today;
    [ObservableProperty]
    DateTime fDiag2 = DateTime.Today;
    [ObservableProperty]
    public int diagPpal = -1;
    [ObservableProperty]
    public bool dsbl = true;
    [ObservableProperty]
    public bool diagVsbl = false;
    [ObservableProperty]
    public bool fVsbl = false;
    [ObservableProperty]
    public string sexG;

    partial void OnDiagPpalChanged(int value)
    {
        if (value == -1)
        {
            FVsbl = false;
            DiagVsbl = false;
            return;
        }
        bool aux = value == 13;
        FVsbl = !aux;
        DiagVsbl = aux;
    }

    [RelayCommand]
    async Task NewUser()
    {        
        if (NombPac != string.Empty)
        {
            Run = true;
            if (Dsbl == true)
            {            
                try
                {                    
                    await firebaseConnecty.RegistPac(EmailPac, "12345678", NombPac);
                    var fecha = FDiag.ToShortDateString();
                    var fecha1 = FDiag1.ToShortDateString();
                    var fecha2 = FDiag2.ToShortDateString();
                    if (DiagPpal == 13)
                    {
                        fecha = null;                        
                    }
                    if (OtroDiag1 == string.Empty | OtroDiag1 == null)
                    {
                        fecha1 = null;
                    }
                    if (OtroDiag2 == string.Empty | OtroDiag2 == null)
                    {
                        fecha2 = null;
                    }
                    var NuevoPaciente = new Paciente
                    {
                        IdMed = firebaseConnecty.userInfo.Uid,
                        Nombre = NombPac,
                        Apelld = ApllPac,
                        Correo = EmailPac,
                        Gener = SexG,
                        FechNac = FNac.ToShortDateString(),
                        Diagnsc = DiagPpal,                        
                        FechDiag = fecha,
                        OtroDiag1 = OtroDiag1,
                        FechDiag1 = fecha1,
                        OtroDiag2 = OtroDiag2,
                        FechDiag2 = fecha2,
                        Pass = "12345678"
                    };
                    await firebaseConnecty.SavePac(firebaseConnecty.pacInfo.Uid,NuevoPaciente);
                    await App.Current.MainPage.DisplayAlert("Correcto", "Se ha creado paciente: " + NombPac, "Ok");
                    NombPac = ApllPac = EmailPac = OtroDiag1 = OtroDiag2 = string.Empty;
                    FNac = FDiag = FDiag1 = FDiag2 = DateTime.Today;
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
                        await App.Current.MainPage.DisplayAlert("Aviso", "Favor ingresa un correo para el usuario nuevo.", "Ok");
                    }
                    if (ex.Message.Contains("INVALID_EMAIL"))
                    {
                        await App.Current.MainPage.DisplayAlert("Aviso", "El correo proporcionado no es valido", "Ok");
                    }
                    if (ex.Message.Contains("EMAIL_EXISTS"))
                    {
                        await App.Current.MainPage.DisplayAlert("Aviso", "Ya existe un paciente con el correo proporcionado, intenta de nuevo con un correo nuevo.", "Ok");
                    }
                }                
            }
            else if (Dsbl == false)
            {
                try
                {
                    await firebaseConnecty.RegistMed(EmailPac, "0987654", NombPac);                  
                    await App.Current.MainPage.DisplayAlert("Correcto", "Se ha creado personal médico: " + NombPac + ". Se ha enviado correo de autenticación al correo del usuario.", "Ok");
                    NombPac = ApllPac = EmailPac = string.Empty;
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("MISSING_EMAIL"))
                    {
                        await App.Current.MainPage.DisplayAlert("Aviso", "Favor ingresa un correo para el usuario nuevo.", "Ok");
                    }
                    if (ex.Message.Contains("INVALID_EMAIL"))
                    {
                        await App.Current.MainPage.DisplayAlert("Aviso", "El correo proporcionado no es valido.", "Ok");
                    }
                    if (ex.Message.Contains("EMAIL_EXISTS"))
                    {
                        await App.Current.MainPage.DisplayAlert("Aviso", "Ya existe un personal médico con el correo proporcionado, intenta de nuevo con un correo nuevo.", "Ok");
                    }
                }
            }
            Run = false;
        }
        else
        {
            await App.Current.MainPage.DisplayAlert("Importante", "Favor ingresa el nombre del usuario nuevo.", "Ok");
        }
    }
}
