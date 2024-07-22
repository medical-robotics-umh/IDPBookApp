using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;
using IDPBookApp.Models;
using IDPBookApp.Pages;
using Plugin.CloudFirestore;
using System.Collections.ObjectModel;

namespace IDPBookApp.ViewModel;

public partial class ListaPacViewModel:BaseViewModel
{
    readonly FirebaseConnecty firebaseConnecty;
    public ObservableCollection<Paciente> Pacientes { get; set; } = new();

    private readonly ListaPacViewModel listaPacViewModel;
    static INavigation Navigation => Shell.Current.Navigation;
    public ListaPacViewModel(FirebaseConnecty firebaseConnecty)
    {
        this.firebaseConnecty = firebaseConnecty;
        GetPacientes();
    }
    
    async void GetPacientes()
    {
        Run = true;
        var pacientes = await FirebaseConnecty.GetPacientesModel();
        if (pacientes != null && pacientes.Count > 0) 
        { 
            Pacientes.Clear();
            foreach (var paciente in pacientes)
            {
                Pacientes.Add(paciente);
            }
        }
        Run = false;
    }

    [RelayCommand]
    private async Task GetIdPaciente(Paciente paciente)
    {
        string ans = await App.Current.MainPage.DisplayActionSheet("¿Que acción desear realizar con los datos de " + paciente.Nombre + "?", "Cancelar",null, "Obtener datos", "Eliminar paciente");
        if (ans == "Obtener datos")
        {
            Run = true;
            await firebaseConnecty.ChangePac(paciente.Correo, paciente.Pass, true);
            Run = false;
            await Shell.Current.DisplayAlert("Carga existosa", "Los datos del paciente:\n\n" + paciente.Nombre + " " + paciente.Apelld + ",\n\nhan sido cargados exitosamente.", "Ok");
            await Shell.Current.GoToAsync(nameof(MainPage));            
        }
        else if (ans == "Eliminar paciente")
        {
            bool ans2 = await App.Current.MainPage.DisplayAlert("¡Aviso!", "Si eliminas el paciente, los datos no se podrán recuperar.\n\n¿Confirmas la eliminación del paciente?", "Si", "No");
            if (ans2 == true)
            {
                Run=true;
                await firebaseConnecty.ChangePac(paciente.Correo, paciente.Pass, false);
                await App.Current.MainPage.DisplayAlert("Paciente eliminado", "El paciente:\n\n" + paciente.Nombre + " " + paciente.Apelld + "\n\nha sido eliminado exitosamente", "Ok");
                Run = false;
                await Shell.Current.GoToAsync(nameof(MainPage));
            }   
        }
    }
}
