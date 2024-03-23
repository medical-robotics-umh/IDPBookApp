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

    [RelayCommand]
    async Task NewAnltc()
    {
        Contador++;
        try
        {
            var Nuevo = new AnaliticaModel
            {
                AId="Analitica "+Contador.ToString(),
                AFecha=DateTime.Today.ToShortDateString(),
                AIgG=AIgG,
                AIgG1=AIgG1,
                AIgG2=AIgG2,
                AIgG3=AIgG3,
                AIgG4=AIgG4,
                AHbA1c=AHbA1c,
                AHDL=AHDL,
                ALDL=ALDL,
                ATG=ATG,
                AColesT=AColesT,
                AHemo=AHemo,
                ALinfo=ALinfo,
                ANeuro=ANeuro,
                APlaque=APlaque,
            };
            await CrossCloudFirestore.Current
                             .Instance
                             .Collection("IDPbookDB")
                             .Document(firebaseConnecty.pacInfo.Uid)
                             .Collection("analiticas")
                             .Document("Anltc" + Contador.ToString())
                             .SetAsync(Nuevo);
        }
        catch (Exception ex)
        {
            await App.Current.MainPage.DisplayAlert("Error NewAnltcVM", ex.Message, "Ok");
        }

        var newPage = new PruebasLabPage(prbsLabViewModel)
        {
            BindingContext = new PrbsLabViewModel(firebaseConnecty)
        };
        Navigation.InsertPageBefore(newPage, Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        await Shell.Current.GoToAsync("../..");
    }
}
