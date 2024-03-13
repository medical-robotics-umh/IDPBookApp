using CommunityToolkit.Mvvm.ComponentModel;
using IDPBookApp.Models;
using System.Collections.ObjectModel;

namespace IDPBookApp.ViewModel;

[QueryProperty(nameof(Episodio), nameof(Episodio))]
public partial class DetailPageViewModel : BaseViewModel
{
    public DetailPageViewModel()
    {
        _ = SeleSin();
    }

    [ObservableProperty]
    EpisodioModel episodio;

    [ObservableProperty]
    ObservableCollection<object> sinCatSelect = new();
    [ObservableProperty]
    ObservableCollection<object> sinDigSelect = new();
    [ObservableProperty]
    ObservableCollection<object> sinUriSelect = new();
    [ObservableProperty]
    ObservableCollection<object> sinCutSelect = new();

    private async Task SeleSin()
    {
        await Task.Delay(1000);
        foreach (var item in ListaSinCat)
        {
            var index = ListaSinCat.IndexOf(item.ToString());
            if (Episodio.ESinCata[index]==true)
            {
                SinCatSelect.Add(ListaSinCat[index]);
            }
        }

        foreach (var item in ListaSinDigest)
        {
            var index = ListaSinDigest.IndexOf(item.ToString());
            if (Episodio.ESinDigest[index] == true)
            {
                SinDigSelect.Add(ListaSinDigest[index]);
            }
        }

        foreach (var item in ListaSinUri)
        {
            var index = ListaSinUri.IndexOf(item.ToString());
            if (Episodio.ESinUri[index] == true)
            {
                SinUriSelect.Add(ListaSinUri[index]);
            }
        }

        foreach (var item in ListaSinCut)
        {
            var index = ListaSinCut.IndexOf(item.ToString());
            if (Episodio.ESinCut[index] == true)
            {
                SinCutSelect.Add(ListaSinCut[index]);
            }
        }
    }
}
