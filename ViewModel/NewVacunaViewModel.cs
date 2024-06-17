using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;
using IDPBookApp.Models;
using IDPBookApp.Pages;
using Plugin.CloudFirestore;

namespace IDPBookApp.ViewModel;

public partial class NewVacunaViewModel : BaseViewModel
{
    readonly FirebaseConnecty firebaseConnecty;
    private readonly VacunasViewModel viewModel;
    static INavigation Navigation => Shell.Current.Navigation;
    public NewVacunaViewModel(FirebaseConnecty firebaseConnecty)
    {
        this.firebaseConnecty = firebaseConnecty;
    }

    [ObservableProperty]
    public string vNmbre;
    [ObservableProperty]
    public string vDosis;
    [ObservableProperty]
    public string vAnoUD;

    [RelayCommand]
    async Task NewVacuna()
    {
        if (VNmbre == string.Empty || VNmbre == null)
        {
            await App.Current.MainPage.DisplayAlert("Campo vacio.","Ingresar nombre de la vacuna antes de guardar.","Ok");
        }
        else
        {
            Run = true;
            try
            {
                var NuevaVacuna = new Vacuna
                {
                    VFecha = DateTime.Today.ToShortDateString(),
                    VNmbre = VNmbre,
                    VDosis = VDosis,
                    VAnoUD = VAnoUD
                };
                await CrossCloudFirestore.Current
                                 .Instance
                                 .Collection("IDPbookDB")
                                 .Document(firebaseConnecty.pacInfo.Uid)
                                 .Collection("vacunas")
                                 .Document("Vcn" + Contador.ToString())
                                 .SetAsync(NuevaVacuna);
                Contador++;
            }
            catch
            {
                await App.Current.MainPage.DisplayAlert("Algo salio mal", $"Se ha producido un error en:\n NewVacunaViewModel", "Aceptar");
            }
            Run = false;
            var newPage = new VacunasPage(viewModel)
            {
                BindingContext = new VacunasViewModel(firebaseConnecty)
            };
            Navigation.InsertPageBefore(newPage, Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
            await Shell.Current.GoToAsync("../..");
        }
    }
}
