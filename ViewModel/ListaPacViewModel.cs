using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;
using IDPBookApp.Models;

namespace IDPBookApp.ViewModel;

public partial class ListaPacViewModel:BaseViewModel
{
    readonly FirebaseConnecty firebaseConnecty;
    public ListaPacViewModel(FirebaseConnecty firebaseConnecty)
    {
        this.firebaseConnecty = firebaseConnecty;
        GetPacientes();
    }
    async void GetPacientes()
    {
        var pacientes = await FirebaseConnecty.GetPacientesModel(firebaseConnecty.userInfo.Uid);
        if (pacientes != null && pacientes.Count > 0) 
        { 
            Pacientes.Clear();
            foreach (var paciente in pacientes)
            {
                Pacientes.Add(paciente);
            }
        }
    }

    [RelayCommand]
    private async Task GetIdPaciente(Paciente paciente)
    {
        Paciente = paciente;
        await Shell.Current.DisplayAlert("Aviso","Paciente "+Paciente.Nombre+", se ha descargado exitosamente", "Ok");
    }


}
