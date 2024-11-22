using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;
using IDPBookApp.Models;
using IDPBookApp.Pages;

namespace IDPBookApp.ViewModel;

[QueryProperty(nameof(ValidCuest), nameof(ValidCuest))]
public partial class NuevaEncuestaViewModel(FirebaseConnecty firebaseConnecty) : BaseViewModel
{
    readonly FirebaseConnecty firebaseConnecty = firebaseConnecty;
    private readonly EncuestasViewModel encuestasViewModel;
    static INavigation Navigation => Shell.Current.Navigation;

    [ObservableProperty]
    public int qMovil = -1;
    [ObservableProperty]
    public int qCuid = -1;
    [ObservableProperty]
    public int qActDia = -1;
    [ObservableProperty]
    public int qDolor = -1;
    [ObservableProperty]
    public int qAnsd = -1;
    [ObservableProperty]
    public double qEscala = 0;
    [ObservableProperty]
    public string qDesc;

    [RelayCommand]
    async Task NewCuest()
    {
        Run = true;
        Contador++;
        var id = "Cuest" + DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
        var NuevoCuest = new Cuestionario
        {
            QId = id,
            QName = "Cuestionario " + Contador.ToString(),
            QFecha = DateTime.Today.ToString("dd/MM/yyyy"),
            QMovil = QMovil,
            QCuid = QCuid,
            QActDia = QActDia,
            QDolor = QDolor,
            QAnsd = QAnsd,
            QEscala = Math.Round(QEscala, 0),
            QDesc = QDesc
        };
        await firebaseConnecty.SaveData(firebaseConnecty.pacInfo.Uid, "cuestionarios", id, NuevoCuest);
        Run = false;
        if (ValidCuest == true)
        {
            await Shell.Current.DisplayAlert("Cuestionario registrado", "Los datos se han guardado correctamente.", "Ok");
            var newPage = new EncuestasPage(encuestasViewModel)
            {
                BindingContext = new EncuestasViewModel(firebaseConnecty)
            };
            Navigation.InsertPageBefore(newPage, Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
            await Shell.Current.GoToAsync("../..");
        }
        else
        {
            await Shell.Current.DisplayAlert("Cuestionario registrado", "Ya puedes seguir haciendo uso de la aplicación.", "Ok");
            await Shell.Current.GoToAsync("/MainPage");
        }
    }
    public override async Task OnBackButtonPressedAsync()
    {
        if (ValidCuest == true)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
