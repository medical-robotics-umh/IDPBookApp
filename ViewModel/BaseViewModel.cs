using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IDPBookApp.Models;

namespace IDPBookApp.ViewModel;

[QueryProperty(nameof(Contador), nameof(Contador))]
[QueryProperty(nameof(Trat), nameof(Trat))]
public partial class BaseViewModel : ObservableObject
{
    public BaseViewModel()
    {

    }
    [ObservableProperty]
    public string nPac;

    [ObservableProperty]
    string nMed;

    [ObservableProperty]
    string blanco = "#FFFFFF";
    [ObservableProperty]
    string grisC = "#F2F4F4";
    [ObservableProperty]
    string azul = "#2E86C1";
    [ObservableProperty]
    string verde = "#28B463";
    [ObservableProperty]
    string nar = "#E67E22";
    [ObservableProperty]
    string amar = "#F4D03F";
    [ObservableProperty]
    string grisO = "#5D6D7E";

    [ObservableProperty]
    int contador;

    [ObservableProperty]
    string trat;

    [ObservableProperty]
    bool validCuest;

    [ObservableProperty]
    bool auth;

    [ObservableProperty]
    Paciente paciente;

    [ObservableProperty]
    private bool disable = true;
    
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

    [ObservableProperty]
    public bool run = false;

    [ObservableProperty]
    private bool graf_visbl;
    [ObservableProperty]
    private bool btn_visbl = false;

    [RelayCommand]
    private void VisibleGraf()
    {
        Graf_visbl = !Graf_visbl;
    }

    [RelayCommand]
    static async Task Navegar(string ruta)
    {
        await Shell.Current.GoToAsync(ruta);
    }

    [RelayCommand]
    async static Task BackBtn()
    {
        await Shell.Current.GoToAsync("..");
    }
}
