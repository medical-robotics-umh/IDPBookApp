using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;
using IDPBookApp.Models;
using IDPBookApp.Pages;
using System.Collections.ObjectModel;

namespace IDPBookApp.ViewModel;

public partial class TratViewModel : BaseViewModel
{
    readonly FirebaseConnecty firebaseConnecty;
    public TratViewModel(FirebaseConnecty firebaseConnecty)
    {
        this.firebaseConnecty = firebaseConnecty;
        VsblTrat = firebaseConnecty.userInfo.IsEmailVerified;
        GetTrat();
    }

    [ObservableProperty]
    Tratamiento tratamiento;

    [ObservableProperty]
    public bool vsblTrat;
    [ObservableProperty]
    public bool vsblAct;

    async void GetTrat()
    {
        Tratamiento = await FirebaseConnecty.GetTratInmunoModel(firebaseConnecty.pacInfo.Uid);
        if (Tratamiento == null)
        {
            VsblAct = false;
        }
    }
}
