using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IDPBookApp.Models;
using System.Collections.ObjectModel;

namespace IDPBookApp.ViewModel;

[QueryProperty(nameof(Contador), nameof(Contador))]
public partial class BaseViewModel : ObservableObject
{
    public BaseViewModel()
    {

    }
    [ObservableProperty]
    string nPac;

    [ObservableProperty]
    string nMed;

    [ObservableProperty]
    int contador;

    [ObservableProperty]
    bool auth;

    [ObservableProperty]
    Paciente paciente;        

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
    [ObservableProperty]

    ObservableCollection<string> listaDiagcs = new()
    {
        "Inmunodeficiencia Común Variable",
        "Agammaglobulinemia",
        "Déficit de IgA",
        "Déficit de Subclases de Inmunoglobulinas",
        "Déficit de respuesta a antígenos específicos",
        "Inmunodeficiencia Combinada",
        "Enfermedad Granulomatosa Crónica",
        "Microdelección 22q",
        "Síndrome de Wiscott-Aldrich",
        "Síndrome de Hiper-IgE",
        "Síndrome de Hiper-IgM",
        "ALPS",
        "Candidiasis Mucocutanea Crónica"
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
