using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
        vsblHis = firebaseConnecty.userInfo.IsEmailVerified;
        GetHisto();
    }
    public ObservableCollection<HistoriaModel> Historias { get; set; } = new();
    [ObservableProperty]
    public bool vsblHis;

    async void GetHisto()
    {
        var historias = await FirebaseConnecty.GetHistoriasModel(firebaseConnecty.pacInfo.Uid);
        if (historias != null && historias.Count > 0)
        {
            Historias.Clear();
            foreach (var historia in historias)
            {
                Historias.Add(historia);
            }
        }
    }

    [RelayCommand]
    async Task GoToNewHisto()
    {
        await Shell.Current.GoToAsync($"{nameof(NHistorial)}?Contador={Historias.Count}");
    }

    [RelayCommand]
    async Task NavHistoDtailAsync(HistoriaModel historia)
    {
        if (historia is null)
            return;

        await Shell.Current.GoToAsync($"{nameof(HistoDetailPage)}", true,
            new Dictionary<string, object>
            {
                {"Historia",historia}
            });
    }
}
