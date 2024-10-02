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
        GetTrat();
    }

    [ObservableProperty]
    Tratamiento tratamiento;

    [ObservableProperty]
    public bool vsblAct;
    [ObservableProperty]
    public bool vsblAct2;
    [ObservableProperty]
    public bool subvsbl;
    [ObservableProperty]
    public bool intvsbl;

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
        }
        else
        {
            VsblAct = false;
            VsblAct2 = true;
        }
        Run = false;
    }
}
