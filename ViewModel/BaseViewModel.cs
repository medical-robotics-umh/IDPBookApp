using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IDPBookApp.Models;
using System.Collections.ObjectModel;

namespace IDPBookApp.ViewModel;

public partial class BaseViewModel : ObservableObject
{
    public BaseViewModel()
    {

    }    

    [ObservableProperty]
    int contador;

    [ObservableProperty]
    bool auth;
    public ObservableCollection<EpisodioModel> Episodios { get; set; } = new ObservableCollection<EpisodioModel>();

    [ObservableProperty]
    ObservableCollection<string> listaSinCat = new()
    {
        "Tos",
        "Moco",
        "Dolor de garganta",
        "Dolor torácico",
        "Sensación de ahogo"
    };

    [ObservableProperty]
    ObservableCollection<string> listaSinDigest = new()
    {
        "Diarrea",
        "Nauseas o vómitos",
        "Estreñimiento",
        "Dolor abdominal"
    };

    [ObservableProperty]
    ObservableCollection<string> listaSinUri = new()
    {
        "Escozor al orinar",
        "Orina oscura",
        "Orina maloliente"
    };

    [ObservableProperty]
    ObservableCollection<string> listaSinCut = new()
    {
        "Picor",
        "Dolor",
        "Coloración rojiza"
    };

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
