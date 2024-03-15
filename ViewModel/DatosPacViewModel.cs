using CommunityToolkit.Mvvm.ComponentModel;
using IDPBookApp.DataBase;
using IDPBookApp.Models;

namespace IDPBookApp.ViewModel;
public partial class DatosPacViewModel : BaseViewModel
{
    readonly FirebaseConnecty firebaseConnecty;
    public DatosPacViewModel(FirebaseConnecty firebaseConnecty)
    {
        this.firebaseConnecty = firebaseConnecty;
        GetPac();
    }

    async void GetPac()
    {
        Paciente = await FirebaseConnecty.GetPacienteModel(firebaseConnecty.pacInfo.Uid);
    }
}
