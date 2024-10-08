using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;
using IDPBookApp.Models;
using IDPBookApp.Pages;
using Plugin.CloudFirestore;

namespace IDPBookApp.ViewModel;

[QueryProperty(nameof(ValidCuest), nameof(ValidCuest))]
public partial class NuevaEncuestaViewModel : BaseViewModel
{
    readonly FirebaseConnecty firebaseConnecty;
    private readonly EncuestasViewModel encuestasViewModel;
    static INavigation Navigation => Shell.Current.Navigation;
    public NuevaEncuestaViewModel(FirebaseConnecty firebaseConnecty)
    {
        this.firebaseConnecty = firebaseConnecty;
    }

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
    async Task NewHisto()
    {
        Run = true;
        Contador++;
        try
        {
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
                QEscala = Math.Round(QEscala,0),
                QDesc = QDesc
            };
            await CrossCloudFirestore.Current
                             .Instance
                             .Collection("IDPbookDB")
                             .Document(firebaseConnecty.pacInfo.Uid)
                             .Collection("cuestionarios")
                             .Document(id)
                             .SetAsync(NuevoCuest);
        }
        catch
        {
            await App.Current.MainPage.DisplayAlert("Algo salio mal", $"Se ha producido un error en:\n NuevaEncuestaViewModel", "Aceptar");
        }
        Run = false;
        if (ValidCuest==true)
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
            await Shell.Current.GoToAsync("..");
        }
    }
}
