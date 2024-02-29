using CommunityToolkit.Mvvm.ComponentModel;
using IDPBookApp.DataBase;
using IDPBookApp.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace IDPBookApp.ViewModel;

[QueryProperty(nameof(Episodio),nameof(Episodio))]
public partial class DetailPageViewModel : BaseViewModel
{
    [ObservableProperty]
    ObservableCollection<object> selectSinCat;
    public DetailPageViewModel()
    {
        selectSinCat = new ObservableCollection<object>()
        {
            Items[1]
        };
    }

    [ObservableProperty]
    EpisodioModel episodio;    

}
