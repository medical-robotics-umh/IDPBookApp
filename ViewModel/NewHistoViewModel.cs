using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;
using IDPBookApp.Models;
using IDPBookApp.Pages;
using Plugin.CloudFirestore;

namespace IDPBookApp.ViewModel;

public partial class NewHistoViewModel : BaseViewModel
{
    readonly FirebaseConnecty firebaseConnecty;
    private readonly HistoViewModel histoViewModel;
    static INavigation Navigation => Shell.Current.Navigation;
    public NewHistoViewModel(FirebaseConnecty firebaseConnecty)
    {
        this.firebaseConnecty = firebaseConnecty;
    }
    
    [ObservableProperty]
    public bool eInf;
    [ObservableProperty]
    public bool eHema;
    [ObservableProperty]
    public bool eADig;
    [ObservableProperty]
    public bool ePulm;
    [ObservableProperty]
    public bool eHepa;
    [ObservableProperty]
    public bool eOnco;
    [ObservableProperty]
    public bool eEndo;
    [ObservableProperty]
    public bool eCardio;
    [ObservableProperty]
    public bool eAuto;
    [ObservableProperty]
    public bool eNeuro;
    [ObservableProperty]
    public bool eCut;

    [ObservableProperty]
    string fDiag;
    [ObservableProperty]
    public bool activ;
    [ObservableProperty]
    public int sInf = -1;
    [ObservableProperty]
    public int sHema = -1;
    [ObservableProperty]
    public int sADig = -1;
    [ObservableProperty]
    public int sPulm = -1;
    [ObservableProperty]
    public int sHepa = -1;
    [ObservableProperty]
    public int sOnco = -1;
    [ObservableProperty]
    public int sEndo = -1;
    [ObservableProperty]
    public int sCardio = -1;
    [ObservableProperty]
    public int sAuto = -1;
    [ObservableProperty]
    public int sNeuro = -1;
    [ObservableProperty]
    public int sCut = -1;
    [ObservableProperty]
    public string otroDiag;
    [ObservableProperty]
    public string comment;
    [ObservableProperty]
    public string aler;

    [RelayCommand]
    async Task NewHisto()
    {
        Run = true;
        Contador++;
        try
        {
            var NuevaHisto = new HistoriaModel
            {
                HId = "Historia "+Contador.ToString(),
                Hfecha = DateTime.Today.ToShortDateString(),
                HfDiag = FDiag,
                HActivo = Activ,
                HTDiag = new bool[] {EInf,EHema,EADig,EPulm,EHepa,EOnco,EEndo,ECardio,EAuto,ENeuro,ECut},
                HTDiagSub = new int[] {SInf,SHema,SADig,SPulm,SHepa,SOnco,SEndo,SCardio,SAuto,SNeuro,SCut},
                HTDiagChar = OtroDiag,
                HTDiagSubChar = Comment,
                HAlerg = Aler
            };
            await CrossCloudFirestore.Current
                             .Instance
                             .Collection("IDPbookDB")
                             .Document(firebaseConnecty.pacInfo.Uid)
                             .Collection("historial")
                             .Document("Hstr"+Contador.ToString())
                             .SetAsync(NuevaHisto);            
        }
        catch
        {
            await App.Current.MainPage.DisplayAlert("Algo salio mal", $"Se ha producido un error en:\n NewHistoViewModel", "Aceptar");
        }
        Run = false;
        var newPage = new HistorialPage(histoViewModel)
        {
            BindingContext = new HistoViewModel(firebaseConnecty)
        };
        Navigation.InsertPageBefore(newPage, Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        await Shell.Current.GoToAsync("../..");
    }
}
