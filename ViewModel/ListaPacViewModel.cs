using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;
using IDPBookApp.Models;
using IDPBookApp.Pages;

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
        await firebaseConnecty.ChangePac(paciente.Correo,"12345678");
        await Shell.Current.DisplayAlert("Aviso","Paciente "+paciente.Nombre+", se ha descargado exitosamente", "Ok");
    }
}
