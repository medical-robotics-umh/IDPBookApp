using CommunityToolkit.Mvvm.ComponentModel;
using IDPBookApp.DataBase;
using IDPBookApp.Models;

namespace IDPBookApp.ViewModel;

public partial class TratViewModel : BaseViewModel
{
    readonly FirebaseConnecty firebaseConnecty;
    public TratViewModel(FirebaseConnecty firebaseConnecty)
    {
        this.firebaseConnecty = firebaseConnecty;
        VsblTrat = firebaseConnecty.userInfo.IsEmailVerified;
        GetTrat();
        Imagen = "ic_logo_inmunoglobulinas_intravenosas.png";
    }

    [ObservableProperty]
    Tratamiento tratamiento;

    [ObservableProperty]
    public bool vsblTrat;
    [ObservableProperty]
    public bool vsblAct;
    [ObservableProperty]
    public bool vsblAct2;
    [ObservableProperty]
    public bool subvsbl;
    [ObservableProperty]
    public bool intvsbl;

    [ObservableProperty]
    public bool ef1;
    [ObservableProperty]
    public bool ef2;
    [ObservableProperty]
    public bool ef3;
    [ObservableProperty]
    public bool ef4;
    [ObservableProperty]
    public bool ef5;
    [ObservableProperty]
    public bool ef6;
    [ObservableProperty]
    public bool ef7;

    [ObservableProperty]
    public string imagen;

    async void GetTrat()
    {
        Run = true;
        Tratamiento = await FirebaseConnecty.GetTratInmunoModel(firebaseConnecty.pacInfo.Uid);
        if (Tratamiento != null)
        {
            VsblAct = true;
            VsblAct2 = false;
            if(Tratamiento.TTipo == 0)
            {
                Subvsbl = false;
                Intvsbl = true;
                Imagen = "intravenous";
            }
            if (Tratamiento.TTipo == 1)
            {
                Intvsbl = false;                
                Subvsbl = true;
                Imagen = "subcutaneous";
            }
            Ef1 = Tratamiento.TEfSec[0];
            Ef2 = Tratamiento.TEfSec[1];
            Ef3 = Tratamiento.TEfSec[2];
            Ef4 = Tratamiento.TEfSec[3];
            Ef5 = Tratamiento.TEfSec[4];
            Ef6 = Tratamiento.TEfSec[5];
            Ef7 = Tratamiento.TEfSec[6];
        }
        else
        {
            VsblAct = false;
            VsblAct2 = true;
        }
        Run = false;
    }
}
