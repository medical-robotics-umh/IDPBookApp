using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;
using IDPBookApp.Models;
using IDPBookApp.Pages;

namespace IDPBookApp.ViewModel;

[QueryProperty(nameof(Vacuna), nameof(Vacuna))]
public partial class VacunaDetailViewModel : BaseViewModel
{
    readonly FirebaseConnecty firebaseConnecty;
    private readonly VacunasViewModel viewModel;
    static INavigation Navigation => Shell.Current.Navigation;

    public VacunaDetailViewModel(FirebaseConnecty firebaseConnecty)
    {
        this.firebaseConnecty = firebaseConnecty;
    }

    [ObservableProperty]
    Vacuna vacuna;

    [RelayCommand]
    async Task ElimVcn()
    {
        bool ans = await Shell.Current.DisplayAlert("¡Aviso!", "Los datos no se podrán recuperar.\n\n¿Confirmas la eliminación del documento?", "Si", "No");
        if (ans == true)
        {
            Run = true;
            await FirebaseConnecty.ElimData(firebaseConnecty.pacInfo.Uid, "vacunas", Vacuna.VId);
            await Shell.Current.DisplayAlert("Vacuna eliminada", "Los datos se han eliminado exitosamente.", "Ok");
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
