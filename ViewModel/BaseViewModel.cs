using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IDPBookApp.Models;
using System.Collections.ObjectModel;

namespace IDPBookApp.ViewModel;

public partial class BaseViewModel : ObservableObject
{
    public BaseViewModel()
    {
        ListaSinCat = new ObservableCollection<string>()
        {
            "Tos",
            "Moco",
            "Dolor de garganta",
            "Dolor torácico",
            "Sensación de ahogo"
        };

        ListaSinDigest = new ObservableCollection<string>()
        {
            "Diarrea",
            "Nauseas o vómitos",
            "Estreñimiento",
            "Dolor abdominal"
        };

        ListaSinUri = new ObservableCollection<string>()
        {
            "Escozor al orinar",
            "Orina oscura",
            "Orina maloliente"
        };

        ListaSinCut = new ObservableCollection<string>()
        {
            "Picor",
            "Dolor",
            "Coloración rojiza"
        };
    }

    

    public ObservableCollection<EpisodioModel> Episodios { get; set; } = new ObservableCollection<EpisodioModel>();

    [ObservableProperty]
    ObservableCollection<string> listaSinCat;

    [ObservableProperty]
    ObservableCollection<string> listaSinDigest;

    [ObservableProperty]
    ObservableCollection<string> listaSinUri;

    [ObservableProperty]
    ObservableCollection<string> listaSinCut;

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
