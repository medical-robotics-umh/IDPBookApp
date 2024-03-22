using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;
using IDPBookApp.Models;
using IDPBookApp.Pages;
using System.Collections.ObjectModel;

namespace IDPBookApp.ViewModel;

public partial class ListViewModel : BaseViewModel
{
    readonly FirebaseConnecty firebaseConnecty;
    public ObservableCollection<EpisodioModel> Episodios { get; set; } = new();
    public ListViewModel(FirebaseConnecty firebaseConnecty)
    {
        this.firebaseConnecty = firebaseConnecty;
        GetEpisodios();
    }
    async void GetEpisodios()
    {
        var episodios = await FirebaseConnecty.GetEpisodiosModel(firebaseConnecty.pacInfo.Uid);
        if (episodios != null && episodios.Count > 0)
        {
            Episodios.Clear();
            foreach (var episodio in episodios)
            {
                Episodios.Add(episodio);
            }
        }
    }

    [RelayCommand]
    async Task Navegar_EpisodioAsync(EpisodioModel episodio)
    {
        if (episodio is null)
            return;

        await Shell.Current.GoToAsync($"{nameof(EpisodioViewPage)}", true,
            new Dictionary<string, object>
            {
                {"Episodio",episodio}
            });
    }

    [RelayCommand]
    async Task NavegarNEpi()
    {
        await Shell.Current.GoToAsync($"{nameof(NEpisPage)}?Contador={Episodios.Count}");
    }
}
