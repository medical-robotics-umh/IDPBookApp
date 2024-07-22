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
        var years = new List<int>();
        int currentYear = DateTime.Now.Year;
        for (int year = currentYear; year >= 1920; year--)
        {
            years.Add(year);
        }
        Years = years.ToArray();
    }

    [ObservableProperty]
    public int[] years;
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
    public bool eOtro;

    [ObservableProperty]
    public object fDiag;
    [ObservableProperty]
    public int fDiagIndx = -1;
    [ObservableProperty]
    public int activ = -1;
    [ObservableProperty]
    public string otroDiag;
    [ObservableProperty]
    public string comment;
    [ObservableProperty]
    public string aler;
    [ObservableProperty]
    public string hTitl;
    [ObservableProperty]
    public int hTDiag = -1;
    [ObservableProperty]
    public int hTDiagSub = -1;

    [RelayCommand]
    async Task NewHisto()
    {
        if (FDiagIndx != -1 & HTDiag != -1)
        {
            Run = true;
            var id = "Hstr" + DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
            try
            {
                if (HTDiag == 11 | HTitl == "Otro")
                {
                    HTitl = OtroDiag;
                }
                var NuevaHisto = new HistoriaModel
                {
                    HId = id,
                    HTitl = HTitl,
                    Hfecha = DateTime.Today.ToShortDateString(),
                    HfDiag = FDiag.ToString(),
                    HActivo = Activ,
                    HTDiag = HTDiag,
                    HTDiagSub = HTDiagSub,
                    HTDiagChar = OtroDiag,
                    HTDiagSubChar = Comment,
                    HAlerg = Aler
                };
                await CrossCloudFirestore.Current
                                 .Instance
                                 .Collection("IDPbookDB")
                                 .Document(firebaseConnecty.pacInfo.Uid)
                                 .Collection("historial")
                                 .Document(id)
                                 .SetAsync(NuevaHisto);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Algo salio mal", ex.Message, "Aceptar");
            }
            await Shell.Current.DisplayAlert("Nueva historia creada", "Los datos se han guardado exitosamente.", "Ok");
            Run = false;            
            var newPage = new HistorialPage(histoViewModel)
            {
                BindingContext = new HistoViewModel(firebaseConnecty)
            };
            Navigation.InsertPageBefore(newPage, Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
            await Shell.Current.GoToAsync("../..");
        }
        else
        {
            await Shell.Current.DisplayAlert("Campos incompletos", "El año y el tipo de diagnostico son campos obligatorios, revisa que se hayan seleccionado correctamente", "Ok");
        }
    }
}
