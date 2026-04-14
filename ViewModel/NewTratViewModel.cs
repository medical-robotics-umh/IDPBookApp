using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;
using IDPBookApp.Models;
using IDPBookApp.Pages;

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
    public int tPrepI = -1;
    [ObservableProperty]
    public int tPrepS = -1;
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
    public string tVolInf;
    [ObservableProperty]
    public object sPrepI;
    [ObservableProperty]
    public object sPrepS;

    [ObservableProperty]
    public bool subvsbl;
    [ObservableProperty]
    public bool intvsbl;
    [ObservableProperty]
    public bool ovsbl;

    [RelayCommand]
    async Task NewTrat()
    {
        if (TTipo == -1 || TCad == -1)
        {
            await Shell.Current.DisplayAlert("Campos incompletos.", "Completa toda la información del tratamiento antes de agregar.", "Ok");
        }
        else
        {
            Run = true;
            var id = "ITrat" + DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
            var name = "Trat";
            var tt = "Trat";
            if (TTipo == 0)
            {
                name = SPrepI.ToString();
                tt = "intra";
            }
            if (TTipo == 1)
            {
                name = SPrepS.ToString();
                tt = "subc";
            }
            var NuevoTrat = new Tratamiento
            {
                TId = id,
                TNom = name,
                TFecha = TFecha.ToString("dd/MM/yyyy"),
                TTs = tt,
                TTipo = TTipo,
                TPrepI = TPrepI,
                TPrepS = TPrepS,
                TDosis = Convert.ToInt32(TDosis),
                TCad = TCad,
                TTimeInf = Convert.ToInt32(TTimeInf),
                TVelInf = Convert.ToInt32(TVelInf),
                TVolInf = Convert.ToInt32(TVolInf)
            };
            await firebaseConnecty.SaveData(firebaseConnecty.pacInfo.Uid, "tratActual", id, NuevoTrat);
            Run = false;
            var newPage = new TratPage(tratView)
            {
                BindingContext = new TratViewModel(firebaseConnecty)
            };
            Navigation.InsertPageBefore(newPage, Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
            await Shell.Current.GoToAsync("../..");
            await Shell.Current.DisplayAlert("Tratamiento creado", "El calendario se abrirá para confirmar recordatorios del tratamiento. Solo presiona 'Guardar' para confirmar.", "Ok");

            var cad = TCad + 1;
            var fecha = TFecha.ToString("yyyyMMdd");
            await Launcher.Default.OpenAsync("https://calendar.google.com/calendar/event?action=TEMPLATE&text=Recordatorio administración de "+name+"&recur=RRULE:FREQ=WEEKLY;INTERVAL="+cad+"&dates="+fecha+"T080000/"+fecha+"T200000&reminder=1d&allDay=true");
        }        
    }
}
