using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;
using IDPBookApp.Models;
using IDPBookApp.Pages;
using System.Collections.ObjectModel;

namespace IDPBookApp.ViewModel;

public partial class PrbsLabViewModel : BaseViewModel
{
    readonly FirebaseConnecty firebaseConnecty;
    public PrbsLabViewModel(FirebaseConnecty firebaseConnecty)
    {
        this.firebaseConnecty = firebaseConnecty;
        GetAnltcs();
    }
    public ObservableCollection<AnaliticaModel> Analiticas { get; set; } = new();

    async void GetAnltcs()
    {
        Run = true;
        var historias = await FirebaseConnecty.GetAnaliticsModel(firebaseConnecty.pacInfo.Uid);
        if (historias != null && historias.Count > 0)
        {
            Analiticas.Clear();
            foreach (var historia in historias)
            {
                Analiticas.Add(historia);
            }
        }
        Run = false;
    }

    [RelayCommand]
    async Task GoToNewAnltc()
    {
        await Shell.Current.GoToAsync($"{nameof(NuevaAnalitica)}?Contador={Analiticas.Count}");
    }

    [RelayCommand]
    async Task NavAnltcDtailAsync(AnaliticaModel analitica)
    {
        Run = true;
        if (analitica is null)
            return;

        await Shell.Current.GoToAsync($"{nameof(AnltcDetailPage)}", true,
            new Dictionary<string, object>
            {
                {"Analitica",analitica}
            });
        Run = false;
    }
}
