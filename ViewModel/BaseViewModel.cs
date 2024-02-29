using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IDPBookApp.Models;
using System.Collections.ObjectModel;

namespace IDPBookApp.ViewModel;
public partial class BaseViewModel : ObservableObject
{
    public BaseViewModel()
    {
        Items = new ObservableCollection<string>()
        {
            "Tos",
            "Moco",
            "Dolor de garganta",
            "Dolor torácico",
            "Sensación de ahogo"
        };
    }

    public ObservableCollection<EpisodioModel> Episodios { get; set; } = new ObservableCollection<EpisodioModel>();
    public ObservableCollection<string> Items { get; set; }

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
