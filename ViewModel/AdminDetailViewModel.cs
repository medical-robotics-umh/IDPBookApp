using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;
using IDPBookApp.Models;

namespace IDPBookApp.ViewModel;

[QueryProperty(nameof(Adminis), nameof(Adminis))]
[QueryProperty(nameof(Elimvsbl), nameof(Elimvsbl))]

public partial class AdminDetailViewModel(FirebaseConnecty firebaseConnecty) : BaseViewModel
{
    readonly FirebaseConnecty firebaseConnecty = firebaseConnecty;

    [ObservableProperty]
    Admin adminis;
    [ObservableProperty]
    bool elimvsbl;

    [RelayCommand]
    async Task EliminarAdmin()
    {
        bool ans = await Shell.Current.DisplayAlert("¡Aviso!", "Los datos no se podrán recuperar.\n\n¿Confirmas la eliminación de la administración?", "Si", "No");
        if (ans == true)
        {
            Run = true;
            await firebaseConnecty.ElimDocs(firebaseConnecty.pacInfo.Uid, "tratActual/"+Trat+"/administraciones", Adminis.AdId);
            await Shell.Current.DisplayAlert("Documento eliminado", "Los datos se han eliminado exitosamente.", "Ok");
            Run = false;
            await Shell.Current.GoToAsync("../..");
        }
    }
}
