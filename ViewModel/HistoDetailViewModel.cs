using CommunityToolkit.Mvvm.ComponentModel;
using IDPBookApp.Models;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDPBookApp.ViewModel;

[QueryProperty("Historia","Historia")]
public partial class HistoDetailViewModel : BaseViewModel
{
    public HistoDetailViewModel()
    {
       _ = Selec();
    }

    [ObservableProperty]
    HistoriaModel historia;

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
    public int sInf;
    [ObservableProperty]
    public int sHema;
    [ObservableProperty]
    public int sADig;
    [ObservableProperty]
    public int sPulm;
    [ObservableProperty]
    public int sHepa;
    [ObservableProperty]
    public int sOnco;
    [ObservableProperty]
    public int sEndo;
    [ObservableProperty]
    public int sCardio;
    [ObservableProperty]
    public int sAuto;
    [ObservableProperty]
    public int sNeuro;
    [ObservableProperty]
    public int sCut;

    private async Task Selec()
    {
        await Task.Delay(50);
        EInf = Historia.HTDiag[0];
        EHema = Historia.HTDiag[1];
        EADig = Historia.HTDiag[2];
        EPulm = Historia.HTDiag[3];
        EHepa = Historia.HTDiag[4];
        EOnco = Historia.HTDiag[5];
        EEndo = Historia.HTDiag[6];
        ECardio = Historia.HTDiag[7];
        EAuto = Historia.HTDiag[8];
        ENeuro = Historia.HTDiag[9];
        ECut = Historia.HTDiag[10];

        SInf = Historia.HTDiagSub[0];
        SHema = Historia.HTDiagSub[1];
        SADig = Historia.HTDiagSub[2];
        SPulm = Historia.HTDiagSub[3];
        SHepa = Historia.HTDiagSub[4];
        SOnco = Historia.HTDiagSub[5];
        SEndo = Historia.HTDiagSub[6];
        SCardio = Historia.HTDiagSub[7];
        SAuto = Historia.HTDiagSub[8];
        SNeuro = Historia.HTDiagSub[9];
        SCut = Historia.HTDiagSub[10];
    }
}
