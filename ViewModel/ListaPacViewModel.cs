using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;
using IDPBookApp.Models;
using IDPBookApp.Pages;
using System.Collections.ObjectModel;

namespace IDPBookApp.ViewModel;

public partial class ListaPacViewModel:BaseViewModel
{
    readonly FirebaseConnecty firebaseConnecty;
    public ObservableCollection<Paciente> Pacientes { get; set; } = new();
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
        await firebaseConnecty.ChangePac(paciente.Correo,paciente.Pass);
        await Shell.Current.DisplayAlert("Descarga existosa.","Los datos del paciente:\n\n"+paciente.Nombre+" "+paciente.Apelld+"\n\nhan sido descargados exitosamente.", "Ok");
        await Shell.Current.GoToAsync(nameof(MainPage));
    }
}
