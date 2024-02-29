using CommunityToolkit.Mvvm.ComponentModel;
using IDPBookApp.DataBase;
using IDPBookApp.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace IDPBookApp.ViewModel;

[QueryProperty(nameof(Episodio),nameof(Episodio))]
public partial class DetailPageViewModel : BaseViewModel
{
    public DetailPageViewModel()
    {

    }

    [ObservableProperty]
    EpisodioModel episodio;
}
