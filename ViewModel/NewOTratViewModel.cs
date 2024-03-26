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

    [RelayCommand]
    async Task NewOtroTrat()
    {        
        try
        {
            var NuevoTrat = new OtroTrat
            {
                OTFecha= DateTime.Today.ToShortDateString(),
                OTNombre = OTNombre,
                OTDosis = OTDosis,
                OTCad = OTCad,
                OTFini = OTFini.ToShortDateString(),
                OTFfin = OTFfin.ToShortDateString()
            };
            await CrossCloudFirestore.Current
                             .Instance
                             .Collection("IDPbookDB")
                             .Document(firebaseConnecty.pacInfo.Uid)
                             .Collection("tratamientos")
                             .Document("OTrat"+Contador.ToString())
                             .SetAsync(NuevoTrat);
            Contador++;
        }
        catch (Exception ex)
        {
            await App.Current.MainPage.DisplayAlert("Error NewOtroTratVM", ex.Message, "Ok");
        }

        var newPage = new OtroTratPage(viewModel)
        {
            BindingContext = new OtroTratViewModel(firebaseConnecty)
        };
        Navigation.InsertPageBefore(newPage, Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        await Shell.Current.GoToAsync("../..");
    }
}
