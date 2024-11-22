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
    public ObservableCollection<AnaliticaModel> Analiticas { get; set; } = [];

    async void GetAnltcs()
    {
        Run = true;
        var anltcs = await firebaseConnecty.GetModelList<AnaliticaModel>(firebaseConnecty.pacInfo.Uid, "analiticas");
        if (anltcs != null && anltcs.Count > 0)
        {
            Analiticas.Clear();
            foreach (var anltca in anltcs)
            {
                Analiticas.Add(anltca);
            }
        }
        if (anltcs.Count>=2)
            Btn_visbl = true;
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
