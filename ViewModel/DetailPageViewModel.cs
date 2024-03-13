using CommunityToolkit.Mvvm.ComponentModel;
using IDPBookApp.Models;
using System.Collections.ObjectModel;

namespace IDPBookApp.ViewModel;

[QueryProperty(nameof(Episodio), nameof(Episodio))]
public partial class DetailPageViewModel : BaseViewModel
{
    public DetailPageViewModel()
    {
        SinCatSelect=new ObservableCollection<object>()
        {
            ListaSinCat[3]
        };
    }

    [ObservableProperty]
    EpisodioModel episodio;

    [ObservableProperty]
    ObservableCollection<object> sinCatSelect;
}
