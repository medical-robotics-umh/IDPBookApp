using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;
using IDPBookApp.Models;
using IDPBookApp.Pages;

namespace IDPBookApp.ViewModel;

public partial class NewVacunaViewModel(FirebaseConnecty firebaseConnecty) : BaseViewModel
{
    readonly FirebaseConnecty firebaseConnecty = firebaseConnecty;
    private readonly VacunasViewModel viewModel;
    static INavigation Navigation => Shell.Current.Navigation;

    [ObservableProperty]
    public string vNmbre;
    [ObservableProperty]
    public string vDosis;
    [ObservableProperty]
    DateTime vAnoUD = DateTime.Today;

    [RelayCommand]
    async Task NewVacuna()
    {
        if (VNmbre == string.Empty || VNmbre == null)
        {
            await Shell.Current.DisplayAlert("Campo vacio.", "Ingresar nombre de la vacuna antes de guardar.", "Ok");
        }
        else
        {
            Run = true;
            var id = "Vcn" + DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
            var NuevaVacuna = new Vacuna
            {
                VId = id,
                VFecha = DateTime.Today.ToString("dd/MM/yyyy"),
                VNmbre = VNmbre,
                VDosis = VDosis,
                VAnoUD = VAnoUD.ToString("dd/MM/yyyy")
            };
            await firebaseConnecty.SaveData(firebaseConnecty.pacInfo.Uid, "vacunas", id, NuevaVacuna);
            Run = false;
            await Shell.Current.DisplayAlert("Nueva vacuna registrada", "Los datos se han guardado exitosamente.", "Ok");
            var newPage = new VacunasPage(viewModel)
            {
                BindingContext = new VacunasViewModel(firebaseConnecty)
            };
            Navigation.InsertPageBefore(newPage, Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
            await Shell.Current.GoToAsync("../..");
        }
    }
}
