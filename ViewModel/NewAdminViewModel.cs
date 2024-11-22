using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;
using IDPBookApp.Models;

namespace IDPBookApp.ViewModel;

public partial class NewAdminViewModel : BaseViewModel
{
    readonly FirebaseConnecty firebaseConnecty;
    public NewAdminViewModel(FirebaseConnecty firebaseConnecty)
    {
        this.firebaseConnecty = firebaseConnecty;
        AdFecha = DateTime.Today;
    }

    [ObservableProperty]
    public DateTime adFecha;
    [ObservableProperty]
    public bool ef0;
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

    [RelayCommand]
    async Task NewAdmin()
    {
        Run = true;
        var id = "Admin" + DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
        Contador++;
        var NuevoTrat = new Admin
        {
            AdId = id,
            AdFecha = AdFecha.ToString("dd/MM/yyyy"),
            AdName = "Administración "+Contador.ToString(),
            Ef0 = Ef0,
            Ef1 = Ef1,
            Ef2 = Ef2,
            Ef3 = Ef3,
            Ef4 = Ef4,
            Ef5 = Ef5,
            Ef6 = Ef6,

        };
        await firebaseConnecty.SaveData(firebaseConnecty.pacInfo.Uid, "tratActual/" + Trat + "/administraciones", id,NuevoTrat);
        Run = false;
        await Shell.Current.DisplayAlert("Administración registrada", "Los datos se han guardado correctamente.", "Ok");
        await Shell.Current.GoToAsync("../..");
    }
}
