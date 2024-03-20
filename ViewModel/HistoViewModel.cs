using IDPBookApp.DataBase;
using IDPBookApp.Models;
using IDPBookApp.Pages;
using System.Collections.ObjectModel;

namespace IDPBookApp.ViewModel;

public partial class HistoViewModel : BaseViewModel
{
    readonly FirebaseConnecty firebaseConnecty;
    public HistoViewModel(FirebaseConnecty firebaseConnecty)
    {
        this.firebaseConnecty = firebaseConnecty;
    }
    public ObservableCollection<HistoriaModel> Historias { get; set; } = new();
}
