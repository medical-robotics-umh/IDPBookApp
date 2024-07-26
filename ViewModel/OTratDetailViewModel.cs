using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;
using IDPBookApp.Models;
using IDPBookApp.Pages;

namespace IDPBookApp.ViewModel;

[QueryProperty(nameof(Tratamiento), nameof(Tratamiento))]
public partial class OTratDetailViewModel : BaseViewModel
{
    readonly FirebaseConnecty firebaseConnecty;
    private readonly OtroTratViewModel viewModel;
    static INavigation Navigation => Shell.Current.Navigation;

    public OTratDetailViewModel(FirebaseConnecty firebaseConnecty)
    {
        this.firebaseConnecty = firebaseConnecty;
    }

    [ObservableProperty]
    OtroTrat tratamiento;

    [RelayCommand]
    async Task EliminarTrat()
    {
        bool ans = await Shell.Current.DisplayAlert("¡Aviso!", "Los datos no se podrán recuperar.\n\n¿Confirmas la eliminación del documento?", "Si", "No");
        if (ans == true)
        {
            Run = true;
            await FirebaseConnecty.ElimData(firebaseConnecty.pacInfo.Uid, "tratamientos", Tratamiento.Id);
            await Shell.Current.DisplayAlert("Tratamiento eliminado", "Los datos se han eliminado exitosamente.", "Ok");
            Run = false;
            var newPage = new OtroTratPage(viewModel)
            {
                BindingContext = new OtroTratViewModel(firebaseConnecty)
            };
            Navigation.InsertPageBefore(newPage, Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
            await Shell.Current.GoToAsync("../..");
        }
    }
}
