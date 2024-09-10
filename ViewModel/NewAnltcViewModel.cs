using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;
using IDPBookApp.Models;
using IDPBookApp.Pages;
using Plugin.CloudFirestore;

namespace IDPBookApp.ViewModel;

public partial class NewAnltcViewModel : BaseViewModel
{
    readonly FirebaseConnecty firebaseConnecty;
    private readonly PrbsLabViewModel prbsLabViewModel;
    static INavigation Navigation => Shell.Current.Navigation;
    public NewAnltcViewModel(FirebaseConnecty firebaseConnecty)
    {
        this.firebaseConnecty = firebaseConnecty;
    }

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
        try
        {
            var id = "Anltc" + DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
            //DateTimeOffset dateTimeOffset = Date;
            //var id2 = "Anltc" + dateTimeOffset.ToUnixTimeSeconds().ToString();
            var Nuevo = new AnaliticaModel
            {
                AId = id,
                AName="Analítica "+Contador.ToString(),
                AFecha=Date.ToShortDateString(),
                AIgG=Convert.ToInt32(AIgG),
                AIgG1= Convert.ToInt32(AIgG1),
                AIgG2= Convert.ToInt32(AIgG2),
                AIgG3= Convert.ToInt32(AIgG3),
                AIgG4= Convert.ToInt32(AIgG4),
                AHbA1c= Convert.ToInt32(AHbA1c),
                AHDL= Convert.ToInt32(AHDL),
                ALDL= Convert.ToInt32(ALDL),
                ATG= Convert.ToInt32(ATG),
                AColesT= Convert.ToInt32(AColesT),
                AHemo= Convert.ToInt32(AHemo),
                ALinfo= Convert.ToInt32(ALinfo),
                ANeuro= Convert.ToInt32(ANeuro),
                APlaque= Convert.ToInt32(APlaque),
            };
            await CrossCloudFirestore.Current
                             .Instance
                             .Collection("IDPbookDB")
                             .Document(firebaseConnecty.pacInfo.Uid)
                             .Collection("analiticas")
                             .Document(id)
                             .SetAsync(Nuevo);
        }
        catch
        {
            await App.Current.MainPage.DisplayAlert("Algo salio mal", $"Se ha producido un error en:\n NewAnltcViewModel", "Aceptar");
        }
        Run = false;
        var newPage = new PruebasLabPage(prbsLabViewModel)
        {
            BindingContext = new PrbsLabViewModel(firebaseConnecty)
        };
        Navigation.InsertPageBefore(newPage, Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        await Shell.Current.GoToAsync("../..");
    }
}
