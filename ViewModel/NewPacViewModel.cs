using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;
using IDPBookApp.Models;
using Plugin.CloudFirestore;

namespace IDPBookApp.ViewModel;

public partial class NewPacViewModel : BaseViewModel
{
    readonly FirebaseConnecty firebaseConnecty;
    public NewPacViewModel(FirebaseConnecty firebaseConnecty)
    {
        this.firebaseConnecty = firebaseConnecty;
        FNac = FDiag = DateTime.Today;
    }

    [ObservableProperty]
    string nombPac;
    [ObservableProperty]
    string apllPac;
    [ObservableProperty]
    string emailPac;
    [ObservableProperty]
    bool sexPac;
    [ObservableProperty]
    string sex;
    [ObservableProperty]
    DateTime fNac;
    [ObservableProperty]
    bool tratPac;
    [ObservableProperty]
    bool[] diagSelec = new bool[6];
    [ObservableProperty]
    string otroDiag;
    [ObservableProperty]
    DateTime fDiag;

    [RelayCommand]
    async Task NewPac()
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
        NombPac = ApllPac = EmailPac =  string.Empty;
        FNac = FDiag = DateTime.Today;
    }

    [RelayCommand]
    private void SelectSex()
    {
        if (Sex == "Masculino")
            SexPac = true;
        SexPac = false;
        Console.WriteLine(SexPac);
    }
}
