using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;
using IDPBookApp.Models;
using IDPBookApp.Pages;

namespace IDPBookApp.ViewModel;

[QueryProperty(nameof(Tratamiento), nameof(Tratamiento))]
public partial class OTratDetailViewModel(FirebaseConnecty firebaseConnecty) : BaseViewModel
{
    readonly FirebaseConnecty firebaseConnecty = firebaseConnecty;
    private readonly OtroTratViewModel viewModel;
    static INavigation Navigation => Shell.Current.Navigation;

    [ObservableProperty]
    OtroTrat tratamiento;

    [RelayCommand]
    async Task EliminarTrat()
    {
        bool ans = await Shell.Current.DisplayAlert("¡Aviso!", "Los datos no se podrán recuperar.\n\n¿Confirmas la eliminación del documento?", "Si", "No");
        if (ans == true)
        {
            Run = true;
            await firebaseConnecty.ElimDocs(firebaseConnecty.pacInfo.Uid, "otrosTrat", Tratamiento.Id);
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
