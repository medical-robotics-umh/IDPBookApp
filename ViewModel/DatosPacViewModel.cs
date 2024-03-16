using CommunityToolkit.Mvvm.ComponentModel;
using IDPBookApp.DataBase;

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
    async void GetPac()
    {
        //HACK Falta una condición que evite entrar a la info si el paciente es anonimo
        Paciente = await FirebaseConnecty.GetPacienteModel(firebaseConnecty.pacInfo.Uid);
        if(Paciente != null)
        {
            var nac = DateTime.Parse(Paciente.FechNac);
            int añosTranscurridos = DateTime.Today.Year - nac.Year;
            if (DateTime.Today.Month < nac.Month || (DateTime.Today.Month == nac.Month && DateTime.Today.Day < nac.Day))
            {
                añosTranscurridos--; // Resta un año si el cumpleaños aún no ha ocurrido este año
            }
            Fecha = añosTranscurridos;
        }        
    }
}
