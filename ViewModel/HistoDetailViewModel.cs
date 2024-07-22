using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;
using IDPBookApp.Models;
using IDPBookApp.Pages;

namespace IDPBookApp.ViewModel;

[QueryProperty("Historia", "Historia")]
public partial class HistoDetailViewModel : BaseViewModel
{
    readonly FirebaseConnecty firebaseConnecty;
    private readonly HistoViewModel histoViewModel;
    static INavigation Navigation => Shell.Current.Navigation;
    public HistoDetailViewModel(FirebaseConnecty firebaseConnecty)
    {
        this.firebaseConnecty = firebaseConnecty;
        VsblBtn = firebaseConnecty.userInfo.IsEmailVerified;
        _ = Selec();
    }

    [ObservableProperty]
    HistoriaModel historia;

    [ObservableProperty]
    public bool vsblBtn;

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

    private async Task Selec()
    {
        await Task.Delay(50);

        EInf = false;
        EHema = false;
        EADig = false;
        EPulm = false;
        EHepa = false;
        EOnco = false;
        EEndo = false;
        ECardio = false;
        EAuto = false;
        ENeuro = false;
        ECut = false;

        switch (Historia.HTDiag)
        {
            case 0:
                EInf = true;
                break;
            case 1:
                EHema = true;
                break;
            case 2:
                EADig = true;
                break;
            case 3:
                EPulm = true;
                break;
            case 4:
                EHepa = true;
                break;
            case 5:
                EOnco = true;
                break;
            case 6:
                EEndo = true;
                break;
            case 7:
                ECardio = true;
                break;
            case 8:
                EAuto = true;
                break;
            case 9:
                ENeuro = true;
                break;
            case 10:
                ECut = true;
                break;
        }
    }

    [RelayCommand]
    async Task ElimHisto()
    {
        bool ans = await Shell.Current.DisplayAlert("¡Aviso!", "Los datos no se podrán recuperar.\n\n¿Confirmas la eliminación del documento?", "Si", "No");
        if (ans==true)
        {
            Run = true;
            await FirebaseConnecty.ElimData(firebaseConnecty.pacInfo.Uid, "historial", Historia.HId);
            await Shell.Current.DisplayAlert("Historia eliminada", "Los datos se han eliminado exitosamente.", "Ok");
            Run = false;
            var newPage = new HistorialPage(histoViewModel)
            {
                BindingContext = new HistoViewModel(firebaseConnecty)
            };            
            Navigation.InsertPageBefore(newPage, Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
            await Shell.Current.GoToAsync("../..");
        }
    }
}
