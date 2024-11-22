using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;
using IDPBookApp.Models;
using IDPBookApp.Pages;

namespace IDPBookApp.ViewModel;

public partial class NewAnltcViewModel(FirebaseConnecty firebaseConnecty) : BaseViewModel
{
    readonly FirebaseConnecty firebaseConnecty = firebaseConnecty;
    private readonly PrbsLabViewModel prbsLabViewModel;
    static INavigation Navigation => Shell.Current.Navigation;

    [ObservableProperty]
    public string aIgG;
    [ObservableProperty]
    public string aIgG1;
    [ObservableProperty]
    public string aIgG2;
    [ObservableProperty]
    public string aIgG3;
    [ObservableProperty]
    public string aIgG4;
    [ObservableProperty]
    public string aIgA;
    [ObservableProperty]
    public string aIgM;
    [ObservableProperty]
    public string aHbA1c;
    [ObservableProperty]
    public string aHDL;
    [ObservableProperty]
    public string aLDL;
    [ObservableProperty]
    public string aTG;
    [ObservableProperty]
    public string aColesT;
    [ObservableProperty]
    public string aHemo;
    [ObservableProperty]
    public string aLinfo;
    [ObservableProperty]
    public string aNeuro;
    [ObservableProperty]
    public string aPlaque;
    [ObservableProperty]
    public DateTime date = DateTime.Today;

    [RelayCommand]
    async Task NewAnltc()
    {
        Run = true;
        Contador++;
        var id = "Anltc" + DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
        //DateTimeOffset dateTimeOffset = Date;
        //var id2 = "Anltc" + dateTimeOffset.ToUnixTimeSeconds().ToString();
        var NuevaAnltc = new AnaliticaModel
        {
            AId = id,
            AName = "Analítica " + Contador.ToString(),
            AFecha = Date.ToString("dd/MM/yyyy"),
            AIgG = Convert.ToInt32(AIgG),
            AIgG1 = Convert.ToInt32(AIgG1),
            AIgG2 = Convert.ToInt32(AIgG2),
            AIgG3 = Convert.ToInt32(AIgG3),
            AIgG4 = Convert.ToInt32(AIgG4),
            AIgA = Convert.ToInt32(AIgA),
            AIgM = Convert.ToInt32(AIgM),
            AHbA1c = Convert.ToInt32(AHbA1c),
            AHDL = Convert.ToInt32(AHDL),
            ALDL = Convert.ToInt32(ALDL),
            ATG = Convert.ToInt32(ATG),
            AColesT = Convert.ToInt32(AColesT),
            AHemo = Convert.ToInt32(AHemo),
            ALinfo = Convert.ToInt32(ALinfo),
            ANeuro = Convert.ToInt32(ANeuro),
            APlaque = Convert.ToInt32(APlaque),
        };
        await firebaseConnecty.SaveData(firebaseConnecty.pacInfo.Uid, "analiticas", id, NuevaAnltc);
        Run = false;
        await Shell.Current.DisplayAlert("Nueva Analítica registrada", "Los datos se han guardado exitosamente.", "Ok");
        var newPage = new PruebasLabPage(prbsLabViewModel)
        {
            BindingContext = new PrbsLabViewModel(firebaseConnecty)
        };
        Navigation.InsertPageBefore(newPage, Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        await Shell.Current.GoToAsync("../..");
    }
}
