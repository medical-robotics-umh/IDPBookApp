using CommunityToolkit.Mvvm.ComponentModel;
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
    int fecha;
    [ObservableProperty]
    bool vsbl = true;

    async void GetPac()
    {
        Paciente = await FirebaseConnecty.GetPacienteModel(firebaseConnecty.pacInfo.Uid);
        if(Paciente != null)
        {
            var nac = DateTime.ParseExact(Paciente.FechNac, "d/m/yyyy", CultureInfo.InvariantCulture);
            int añosTranscurridos = DateTime.Today.Year - nac.Year;
            if (DateTime.Today.Month < nac.Month || (DateTime.Today.Month == nac.Month && DateTime.Today.Day < nac.Day))
            {
                añosTranscurridos--; //Resta un año si el cumpleaños aún no ha ocurrido este año
            }
            Fecha = añosTranscurridos;
        }
        if (Paciente.Diagnsc == 13)
        {
            Vsbl = false;
        }
    }
}
