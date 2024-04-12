using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;
using IDPBookApp.Models;
using IDPBookApp.Pages;
using Plugin.CloudFirestore;

namespace IDPBookApp.ViewModel;

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

    [RelayCommand]
    async Task NewHisto()
    {
        Contador++;
        try
        {
            var NuevoCuest = new Cuestionario
            {
                QId = "Cuestionario "+Contador.ToString(),
                QFecha = DateTime.Today.ToShortDateString(),
                QMovil = QMovil,
                QCuid = QCuid,
                QActDia = QActDia,
                QDolor = QDolor,
                QAnsd = QAnsd,
                QEscala = QEscala
            };
            await CrossCloudFirestore.Current
                             .Instance
                             .Collection("IDPbookDB")
                             .Document(firebaseConnecty.pacInfo.Uid)
                             .Collection("cuestionarios")
                             .Document("Cuest"+Contador.ToString())
                             .SetAsync(NuevoCuest);
        }
        catch (Exception ex)
        {
            await App.Current.MainPage.DisplayAlert("Error NewCuestVM", ex.Message, "Ok");
        }

        var newPage = new EncuestasPage(encuestasViewModel)
        {
            BindingContext = new EncuestasViewModel(firebaseConnecty)
        };
        Navigation.InsertPageBefore(newPage, Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        await Shell.Current.GoToAsync("../..");
    }
}
