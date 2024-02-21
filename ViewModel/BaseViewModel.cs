using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;
using IDPBookApp.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace IDPBookApp.ViewModel;
public partial class BaseViewModel : ObservableObject
{
    public BaseViewModel()
    {
    }

    public ObservableCollection<EpisodioModel> Episodios { get; set; } = new ObservableCollection<EpisodioModel>();


    [RelayCommand]
    async static Task Navegar(string ruta)
    {
        await Shell.Current.GoToAsync(ruta);
    }

    [RelayCommand]
    async static Task BackBtn()
    {
        await Shell.Current.GoToAsync("..");
    }
}
