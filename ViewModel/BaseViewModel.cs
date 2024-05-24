using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IDPBookApp.Models;
using System.Collections.ObjectModel;

namespace IDPBookApp.ViewModel;

[QueryProperty(nameof(Contador), nameof(Contador))]
public partial class BaseViewModel : ObservableObject
{
    public BaseViewModel()
    {

    }
    [ObservableProperty]
    string nPac;

    [ObservableProperty]
    string nMed;

    [ObservableProperty]
    int contador;

    [ObservableProperty]
    bool auth;

    [ObservableProperty]
    Paciente paciente;

    [ObservableProperty]
    private bool disable = true;
    [ObservableProperty]
    private bool medCheck;

    [ObservableProperty]
    bool sTos;
    [ObservableProperty]
    bool sMoco;
    [ObservableProperty]
    bool sDGarg;
    [ObservableProperty]
    bool sDTorax;
    [ObservableProperty]
    bool sSAh;
    [ObservableProperty]
    bool sDiar;
    [ObservableProperty]
    bool sNaVo;
    [ObservableProperty]
    bool sEstr;
    [ObservableProperty]
    bool sDAbd;
    [ObservableProperty]
    bool sEscz;
    [ObservableProperty]
    bool sOOsc;
    [ObservableProperty]
    bool sOMal;
    [ObservableProperty]
    bool sPicor;
    [ObservableProperty]
    bool sDolor;
    [ObservableProperty]
    bool sColR;

    [RelayCommand]
    async static Task Navegar(string ruta)
    {
        await Shell.Current.GoToAsync(ruta);
    }

    [RelayCommand]
    async static Task BackBtn()
    {
        await Shell.Current.GoToAsync("..");
    }
}
