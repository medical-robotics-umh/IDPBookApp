using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;
using IDPBookApp.Models;
using IDPBookApp.Pages;

namespace IDPBookApp.ViewModel;

[QueryProperty("Analitica","Analitica")]
public partial class AnltcDetailViewModel : BaseViewModel
{
    readonly FirebaseConnecty firebaseConnecty;
    private readonly PrbsLabViewModel prbsLabViewModel;
    static INavigation Navigation => Shell.Current.Navigation;
    public AnltcDetailViewModel(FirebaseConnecty firebaseConnecty) 
    { 
        this.firebaseConnecty = firebaseConnecty;
    }

    [ObservableProperty]
    AnaliticaModel analitica;

    [RelayCommand]
    async Task ElimHisto()
    {
        bool ans = await Shell.Current.DisplayAlert("¡Aviso!", "Los datos no se podrán recuperar.\n\n¿Confirmas la eliminación del documento?", "Si", "No");
        if (ans == true)
        {
            Run = true;
            await FirebaseConnecty.ElimData(firebaseConnecty.pacInfo.Uid, "analiticas", Analitica.AId);
            await Shell.Current.DisplayAlert("Analítica eliminada", "Los datos se han eliminado exitosamente.", "Ok");
            Run = false;
            var newPage = new PruebasLabPage(prbsLabViewModel)
            {
                BindingContext = new PrbsLabViewModel(firebaseConnecty)
            };
            Navigation.InsertPageBefore(newPage, Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
            await Shell.Current.GoToAsync("../..");
        }
    }
}
