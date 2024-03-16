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
    readonly ListaPacViewModel listaPacViewModel;
    INavigation Navigation => Shell.Current.Navigation;
    public NewPacViewModel(FirebaseConnecty firebaseConnecty)
    {
        this.firebaseConnecty = firebaseConnecty;
    }

    [ObservableProperty]
    string nombPac;
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
    bool tratPac;
    [ObservableProperty]
    bool[] diagSelec = new bool[6];
    [ObservableProperty]
    string otroDiag;
    [ObservableProperty]
    DateTime fDiag = DateTime.Today;

    [RelayCommand]
    async Task NewUser()
    {
        try
        {
            await firebaseConnecty.RegistPac(EmailPac,"12345678",NombPac);
        }
        catch (Exception ex) 
        {
            await App.Current.MainPage.DisplayAlert("Auth", ex.Message, "Ok");
        }
        try
        {
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
                FechDiag = FDiag.ToShortDateString()
            };
            await CrossCloudFirestore.Current
                            .Instance
                            .Collection("IDPbookDB")
                            .Document(firebaseConnecty.pacInfo.Uid)
                            .SetAsync(NuevoPaciente);
        }
        catch (Exception ex)
        {
            await App.Current.MainPage.DisplayAlert("Firestore", ex.Message, "Ok");
        }
        await App.Current.MainPage.DisplayAlert("Correcto", "Se ha creado usuario: "+NombPac, "Ok");
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
}
