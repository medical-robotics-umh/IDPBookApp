using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;
using IDPBookApp.Models;
using IDPBookApp.Pages;
using Plugin.CloudFirestore;
using System.Data;

namespace IDPBookApp.ViewModel;

[QueryProperty(nameof(Contador), nameof(Contador))]
public partial class NewDataViewModel : BaseViewModel
{
    readonly FirebaseConnecty firebaseConnecty;
    readonly ListViewModel listViewModel;

    [ObservableProperty]
    private bool cata_visbl;
    [ObservableProperty]
    private bool digest_visbl;
    [ObservableProperty]
    private bool uri_visbl;
    [ObservableProperty]
    private bool cut_visbl;
    [ObservableProperty]
    private bool trat_visbl;
                
    [ObservableProperty]
    private DateTime fecha_Epis;
    [ObservableProperty]
    private string durac_entry;
    [ObservableProperty]
    private bool aten_bool;
    [ObservableProperty]
    private bool urg_bool;
    [ObservableProperty]
    private bool ingreso_bool;
    [ObservableProperty]
    private bool fiebre_bool;
    [ObservableProperty]
    private bool[] selectSinCat;
    [ObservableProperty]
    private string otroSinCat;
    [ObservableProperty]
    private bool[] selectSinDigest;
    [ObservableProperty]
    private string otroSinDigest;
    [ObservableProperty]
    private bool[] selectSinUri;
    [ObservableProperty]
    private string otroSinUri;
    [ObservableProperty]
    private bool[] selectSinCut;
    [ObservableProperty]
    private string otroSinCut;
    [ObservableProperty]
    private string otroSin;
    [ObservableProperty]
    private bool trat_bool;
    [ObservableProperty]
    private string antibio;
    [ObservableProperty]
    private string antibioDias;
    [ObservableProperty]
    private string otroTrat;

    INavigation Navigation => Shell.Current.Navigation;

    public NewDataViewModel(FirebaseConnecty firebaseConnecty)
    {
        this.firebaseConnecty = firebaseConnecty;
        SelectSinCat = new bool[ListaSinCat.Count];
        SelectSinDigest = new bool[ListaSinDigest.Count];
        SelectSinUri = new bool[ListaSinUri.Count];
        SelectSinCut = new bool[ListaSinCut.Count];
        Fecha_Epis = DateTime.Today;
    }

    //HACK Metodo para subir episodios a Firebase
    [RelayCommand]
    async Task Cargar()
    {
        if(Contador==0)
        {
            await CrossCloudFirestore.Current
                             .Instance
                             .Collection("IDPbookDB")
                             .Document(firebaseConnecty.userInfo.Uid)
                             .SetAsync(new {Date = DateTimeOffset.Now.ToUnixTimeSeconds().ToString()});
        }
        Contador++;
        try
        {
            var NuevoEpisodio = new EpisodioModel
            {
                EId = "Episodio "+Contador.ToString(),
                EFecha = Fecha_Epis.ToShortDateString(),
                EAtenPrim = Aten_bool,
                EUrgHosp = Urg_bool,
                EDurac = Durac_entry,
                EIngreso =  Ingreso_bool,
                EFiebre = Fiebre_bool,
                ESinCata = SelectSinCat,
                ESinCataChar = OtroSinCat,
                ESinDigest = SelectSinDigest,
                ESinDigestChar = OtroSinDigest,
                ESinUri = SelectSinUri,
                ESinUriChar = OtroSinUri,
                ESinCut = SelectSinCut,
                ESinCutChar = OtroSinCut,
                EOtroSin = OtroSin,
                ETrat = Trat_bool,
                ETratAntibio = Antibio,
                ETratDias = AntibioDias,
                ETratOtros = OtroTrat,
            };
            await CrossCloudFirestore.Current
                             .Instance
                             .Collection("IDPbookDB")
                             .Document(firebaseConnecty.userInfo.Uid)
                             .Collection("episodios")
                             .Document("Ep"+Contador.ToString())
                             .SetAsync(NuevoEpisodio);
        }
        catch (Exception ex)
        {
            await App.Current.MainPage.DisplayAlert("Error NewDataVM", ex.Message, "Ok");
        }

        //HACK Función para reemplazar el viewmodel y cargar automaticamente la lista de episodios.
        await Navigation.PopAsync();
        var EP = new EpisodiosPage(listViewModel)
        {
            BindingContext = new ListViewModel(firebaseConnecty)
        };
        Navigation.InsertPageBefore(EP, Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private void VisibleCat()
    {
        Cata_visbl = !Cata_visbl;
    }
    [RelayCommand]
    private void VisibleDigest()
    {
        Digest_visbl = !Digest_visbl;
    }
    [RelayCommand]
    private void VisibleUri()
    {
        Uri_visbl = !Uri_visbl;
    }
    [RelayCommand]
    private void VisibleCutan()
    {
        Cut_visbl = !Cut_visbl;
    }
}