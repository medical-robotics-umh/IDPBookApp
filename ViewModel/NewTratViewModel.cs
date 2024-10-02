using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;
using IDPBookApp.Models;
using IDPBookApp.Pages;
using Plugin.CloudFirestore;

namespace IDPBookApp.ViewModel;

public partial class NewTratViewModel : BaseViewModel
{
    readonly FirebaseConnecty firebaseConnecty;
    private readonly TratViewModel tratView;
    static INavigation Navigation => Shell.Current.Navigation;
    public NewTratViewModel(FirebaseConnecty firebaseConnecty)
    {
        this.firebaseConnecty = firebaseConnecty;
        TFecha = DateTime.Today;
    }

    [ObservableProperty]
    public DateTime tFecha;
    [ObservableProperty]
    public int tTipo = -1;
    [ObservableProperty]
    public int tPrep = -1;
    [ObservableProperty]
    public string tDosis;
    [ObservableProperty]
    public int tCad = -1;
    [ObservableProperty]
    public TimeSpan tHora;
    [ObservableProperty]
    public string tTimeInf;
    [ObservableProperty]
    public string tVelInf;
    [ObservableProperty]
    public string tVolInf;

    [ObservableProperty]
    public bool subvsbl;
    [ObservableProperty]
    public bool intvsbl;
    [ObservableProperty]
    public bool ovsbl;

    [RelayCommand]
    async Task NewTrat()
    {
        if (TTipo == -1)
        {
            await App.Current.MainPage.DisplayAlert("Campo incompleto.", "Selecciona el tipo de tratamiento antes de agregar.", "Ok");
        }
        else
        {
            Run = true;
            var NuevoTrat = new Tratamiento
            {
                TFecha = TFecha.ToShortDateString(),
                TTipo = TTipo,
                TPrep = TPrep,
                TDosis = Convert.ToInt32(TDosis),
                TCad = TCad,
                TTimeInf = Convert.ToInt32(TTimeInf),
                TVelInf = Convert.ToInt32(TVelInf),
                TVolInf = Convert.ToInt32(TVolInf)
            };
            await CrossCloudFirestore.Current
                             .Instance
                             .Collection("IDPbookDB")
                             .Document(firebaseConnecty.pacInfo.Uid)
                             .Collection("tratamientos")
                             .Document("InmunoActual")
                             .SetAsync(NuevoTrat);
            Run = false;
            var newPage = new TratPage(tratView)
            {
                BindingContext = new TratViewModel(firebaseConnecty)
            };
            Navigation.InsertPageBefore(newPage, Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
            await Shell.Current.GoToAsync("../..");
        }        
    }
}
