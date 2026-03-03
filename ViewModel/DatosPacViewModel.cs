using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;
using System.Globalization;

namespace IDPBookApp.ViewModel;
public partial class DatosPacViewModel : BaseViewModel
{
    readonly FirebaseConnecty firebaseConnecty;
    public DatosPacViewModel(FirebaseConnecty firebaseConnecty)
    {
        this.firebaseConnecty = firebaseConnecty;
        GetPac();
    }

    [ObservableProperty]
    int edad;
    [ObservableProperty]
    bool vsbl = false;
    [ObservableProperty]
    bool vsbl2 = false;
    [ObservableProperty]
    bool vsbl3 = false;
    [ObservableProperty]
    int diag;
    [ObservableProperty]
    bool editt = false;

    async void GetPac()
    {
        try
        {
            Paciente = await firebaseConnecty.GetPacienteModel(firebaseConnecty.pacInfo.Uid);
            if (Paciente != null)
            {
                var nac = DateTime.ParseExact(Paciente.FechNac, "d/m/yyyy", CultureInfo.InvariantCulture);
                int añosTranscurridos = DateTime.Today.Year - nac.Year;
                if (DateTime.Today.Month < nac.Month || (DateTime.Today.Month == nac.Month && DateTime.Today.Day < nac.Day))
                {
                    añosTranscurridos--; //Resta un año si el cumpleaños aún no ha ocurrido este año
                }
                Edad = añosTranscurridos;
                Diag = Paciente.Diagnsc;
            }
            if (Paciente.Diagnsc != 13)
            {
                Vsbl = true;
            }
            if (Paciente.OtroDiag1 != null)
            {
                Vsbl2 = true;
            }
            if (Paciente.OtroDiag2 != null)
            {
                Vsbl3 = true;
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("AVISO!!!", $"No se pudo obtener paciente: {ex.Message}", "Ok");
        }
    }

    [RelayCommand]
    async Task Edit()
    {
        var aux = await App.Current.MainPage.DisplayAlert("AVISO", "¿Desea actualizar los datos personales?", "Si","No");
        if (aux)
        {
            Editt = true;
        }
        else
        {
            Editt = false;
        }
    }

    [RelayCommand]
    async Task Save()
    {
        Paciente.Diagnsc = Diag;
        await firebaseConnecty.SavePac(firebaseConnecty.pacInfo.Uid, Paciente);
        await App.Current.MainPage.DisplayAlert("Correcto", "Se han actualizado los datos del usuario: " + Paciente.Nombre, "Ok");
        Editt = false;
    }

    partial void OnDiagChanged(int value)
    {
        if (value == -1)
        {
            Vsbl = false;
            Vsbl2 = false;
            Vsbl3 = false;
            return;
        }
        bool aux = value == 13;
        Vsbl = !aux;
        Vsbl2 = aux;
        Vsbl3 = aux;
    }
}
