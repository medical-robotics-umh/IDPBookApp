using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;
using IDPBookApp.Models;
using IDPBookApp.Pages;
using Plugin.CloudFirestore;

namespace IDPBookApp.ViewModel;

public partial class NewTratViewModel : BaseViewModel
{
    readonly FirebaseConnecty firebaseConnecty;
    private readonly TratViewModel tratView;
    static INavigation Navigation => Shell.Current.Navigation;
    public NewTratViewModel(FirebaseConnecty firebaseConnecty)
    {
        this.firebaseConnecty = firebaseConnecty;
        TFecha = DateTime.Today;
    }

    [ObservableProperty]
    public DateTime tFecha;
    [ObservableProperty]
    public int tTipo = -1;
    [ObservableProperty]
    public string tPrep;
    [ObservableProperty]
    public string tDosis;
    [ObservableProperty]
    public int tCad = -1;
    [ObservableProperty]
    public TimeSpan tHora;
    [ObservableProperty]
    public string tTimeInf;
    [ObservableProperty]
    public string tVelInf;

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
    public bool subvsbl;
    [ObservableProperty]
    public bool intvsbl;

    [RelayCommand]
    async Task NewTrat()
    {
        if (TTipo == -1)
        {
            await App.Current.MainPage.DisplayAlert("Campo incompleto.", "Selecciona el tipo de tratamiento antes de agregar.", "Ok");
        }
        else
        {
            var NuevoTrat = new Tratamiento
            {
                TFecha = TFecha.ToShortDateString(),
                TTipo = TTipo,
                TPrep = TPrep,
                TDosis = Convert.ToInt32(TDosis),
                TCad = TCad,
                TEfSec = new bool[] { Ef1, Ef2, Ef3, Ef4, Ef5, Ef6, Ef7 },
                THora = THora.ToString(),
                TTimeInf = Convert.ToInt32(TTimeInf),
                TVelInf = Convert.ToInt32(TVelInf)
            };
            var uno = CrossCloudFirestore.Current
                             .Instance
                             .Collection("IDPbookDB")
                             .Document(firebaseConnecty.pacInfo.Uid)
                             .Collection("tratamientos")
                             .Document("InmunoActual");

            var dos = CrossCloudFirestore.Current
                      .Instance
                      .Collection("IDPbookDB")
                      .Document(firebaseConnecty.pacInfo.Uid);

            await CrossCloudFirestore.Current
                .Instance
                .Batch()
                .Set(uno,NuevoTrat)
                .Update(dos, "TratAct", TTipo)
                .CommitAsync();

            var newPage = new TratPage(tratView)
            {
                BindingContext = new TratViewModel(firebaseConnecty)
            };
            Navigation.InsertPageBefore(newPage, Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
            await Shell.Current.GoToAsync("../..");
        }        
    }
}
