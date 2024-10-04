using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;
using IDPBookApp.Models;
using IDPBookApp.Pages;

namespace IDPBookApp.ViewModel;

[QueryProperty(nameof(Adminis), nameof(Adminis))]

public partial class AdminDetailViewModel : BaseViewModel
{
    readonly FirebaseConnecty firebaseConnecty;
    private readonly TratDetailViewModel viewModel;
    static INavigation Navigation => Shell.Current.Navigation;
    public AdminDetailViewModel(FirebaseConnecty firebaseConnecty)
    {
        this.firebaseConnecty = firebaseConnecty;
    }

    [ObservableProperty]
    Admin adminis;

    [RelayCommand]
    async Task EliminarAdmin()
    {
        bool ans = await Shell.Current.DisplayAlert("¡Aviso!", "Los datos no se podrán recuperar.\n\n¿Confirmas la eliminación del documento?", "Si", "No");
        if (ans == true)
        {
            Run = true;
            await FirebaseConnecty.ElimData(firebaseConnecty.pacInfo.Uid, "tratActual/"+Trat+"/administraciones", Adminis.AdId);
            // si voy a eliminar desde tratamientos anteriores, debo cambiar tratamientios por tratActual
            await Shell.Current.DisplayAlert("Documento eliminado", "Los datos se han eliminado exitosamente.", "Ok");
            Run = false;
            await Shell.Current.GoToAsync("../..");
        }
    }
}
