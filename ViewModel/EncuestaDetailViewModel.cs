using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;
using IDPBookApp.Models;
using IDPBookApp.Pages;

namespace IDPBookApp.ViewModel;

[QueryProperty(nameof(Cuestionario), nameof(Cuestionario))]
public partial class EncuestaDetailViewModel(FirebaseConnecty firebaseConnecty) : BaseViewModel
{
    readonly FirebaseConnecty firebaseConnecty = firebaseConnecty;
    private readonly EncuestasViewModel encuestasViewModel;
    static INavigation Navigation => Shell.Current.Navigation;

    [ObservableProperty]
    Cuestionario cuestionario;

    [RelayCommand]
    async Task ElimCuest()
    {
        if (Contador != 1)
        {
            bool ans = await Shell.Current.DisplayAlert("¡Aviso!", "Los datos no se podrán recuperar.\n\n¿Confirmas la eliminación del documento?", "Si", "No");
            if (ans == true)
            {
                Run = true;
                await firebaseConnecty.ElimDocs(firebaseConnecty.pacInfo.Uid, "cuestionarios", Cuestionario.QId);
                await Shell.Current.DisplayAlert("Cuestionario eliminado", "Los datos se han eliminado exitosamente.", "Ok");
                Run = false;
                var newPage = new EncuestasPage(encuestasViewModel)
                {
                    BindingContext = new EncuestasViewModel(firebaseConnecty)
                };
                Navigation.InsertPageBefore(newPage, Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                await Shell.Current.GoToAsync("../..");
            }
        }
        else
        {
            await Shell.Current.DisplayAlert("¡Aviso!", "No se puede eliminar el cuestionario porque es el único en la base de datos.\n\nAgrega un nuevo cuestionario y posteriormente elimina este cuestionario.", "Ok");
            await Shell.Current.GoToAsync("..");
        }
    }
}
