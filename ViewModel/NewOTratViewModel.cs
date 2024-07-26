using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;
using IDPBookApp.Models;
using IDPBookApp.Pages;
using Plugin.CloudFirestore;

namespace IDPBookApp.ViewModel;

public partial class NewOTratViewModel : BaseViewModel
{
    readonly FirebaseConnecty firebaseConnecty;
    private readonly OtroTratViewModel viewModel;
    static INavigation Navigation => Shell.Current.Navigation;
    public NewOTratViewModel(FirebaseConnecty firebaseConnecty)
    {
        this.firebaseConnecty = firebaseConnecty;
        OTFini = OTFfin = DateTime.Today;
    }

    [ObservableProperty]
    public string oTNombre;
    [ObservableProperty]
    public string oTDosis;
    [ObservableProperty]
    public string oTCad;
    [ObservableProperty]
    public DateTime oTFini;
    [ObservableProperty]
    public DateTime oTFfin;
    [ObservableProperty]
    public int tCronc = -1;
    [ObservableProperty]
    public bool vsbl;

    [RelayCommand]
    async Task NewOtroTrat()
    {
        if (OTNombre == string.Empty || OTNombre == null)
        {
            await App.Current.MainPage.DisplayAlert("Campo vacio.", "Ingresar nombre del tratamiento antes de guardarlo.", "Ok");
        }
        else
        {
            try
            {
                var fecha = OTFfin.ToShortDateString();
                var id = "OTrat" + DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
                if (TCronc == 0 | TCronc == -1)
                {
                    fecha = null;
                }
                var NuevoTrat = new OtroTrat
                {
                    Id = id,
                    OTFecha = DateTime.Today.ToShortDateString(),
                    OTNombre = OTNombre,
                    OTDosis = OTDosis,
                    OTCad = OTCad,
                    OTFini = OTFini.ToShortDateString(),
                    OTFfin = fecha,
                    OTCronc = TCronc
                };
                await CrossCloudFirestore.Current
                                 .Instance
                                 .Collection("IDPbookDB")
                                 .Document(firebaseConnecty.pacInfo.Uid)
                                 .Collection("tratamientos")
                                 .Document(id)
                                 .SetAsync(NuevoTrat);
            }
            catch
            {
                await App.Current.MainPage.DisplayAlert("Algo salio mal", $"Se ha producido un error en:\n NewOTratViewModel", "Aceptar");
            }

            var newPage = new OtroTratPage(viewModel)
            {
                BindingContext = new OtroTratViewModel(firebaseConnecty)
            };
            Navigation.InsertPageBefore(newPage, Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
            await Shell.Current.GoToAsync("../..");
        }
    }
}
