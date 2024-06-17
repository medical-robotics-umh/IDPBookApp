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
        VsblHis = firebaseConnecty.userInfo.IsEmailVerified;
        GetHisto();
    }
    public ObservableCollection<HistoriaModel> Historias { get; set; } = new();

    [ObservableProperty]
    public bool vsblHis;

    async void GetHisto()
    {
        Run = true;
        var historias = await FirebaseConnecty.GetHistoriasModel(firebaseConnecty.pacInfo.Uid);
        if (historias != null && historias.Count > 0)
        {
            Historias.Clear();
            foreach (var historia in historias)
            {
                Historias.Add(historia);
            }
        }
        Run = false;
    }

    [RelayCommand]
    async Task GoToNewHisto()
    {
        await Shell.Current.GoToAsync($"{nameof(NHistorial)}?Contador={Historias.Count}");
    }

    [RelayCommand]
    async Task NavHistoDtailAsync(HistoriaModel historia)
    {
        Run = true;
        if (historia is null)
            return;

        await Shell.Current.GoToAsync($"{nameof(HistoDetailPage)}", true,
            new Dictionary<string, object>
            {
                {"Historia",historia}
            });
        Run = false;
    }
}
