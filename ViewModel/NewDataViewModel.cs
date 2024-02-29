using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;
using IDPBookApp.Models;
using IDPBookApp.Pages;
using Plugin.CloudFirestore;
using System.Collections.ObjectModel;

namespace IDPBookApp.ViewModel;

public partial class NewDataViewModel : BaseViewModel
{
    readonly FirebaseConnecty firebaseConnecty;
    readonly ListViewModel listViewModel;

    public ObservableCollection<string> Items { get; set; }

    [ObservableProperty]
    private bool isExpand;

    [ObservableProperty]
    private bool[] selectItems;

    [ObservableProperty]
    private string durac_entry;

    [ObservableProperty]
    private string enum_entry = "Episodio5";

    [ObservableProperty]
    private bool aten_bool;
    INavigation Navigation => Shell.Current.Navigation;
    public NewDataViewModel(FirebaseConnecty firebaseConnecty)
    {
        this.firebaseConnecty = firebaseConnecty;
        Items = new ObservableCollection<string>()
        {
            "Tos",
            "Moco",
            "Dolor de garganta",
            "Dolor torácico",
            "Sensación de ahogo"
        };

        SelectItems = new bool[Items.Count];
    }

    [RelayCommand]
    async Task Cargar()
    {
        try
        {
            var Dany = new EpisodioModel
            {
                EAtenPrim = Aten_bool,
                EDurac = Durac_entry,
                Enum = "Episodio 5",
                ESinCata = SelectItems
            };
            await CrossCloudFirestore.Current
                             .Instance
                             .Collection("IDPbookDB")
                             .Document("info_pacientes")
                             .Collection("DARL01")
                             .Document("episodios")
                             .Collection("idEpis")
                             .Document(Enum_entry)
                             .SetAsync(Dany);
        }
        catch (Exception ex)
        {
            await App.Current.MainPage.DisplayAlert("Alerta", ex.Message, "Ok");
        }
        //HACK Metodo para reemplazar el viewmodel y cargar automaticamente la lista de objetos.
        await Navigation.PopAsync();
        var EP = new EpisodiosPage(listViewModel)
        {
            BindingContext = new ListViewModel(firebaseConnecty)
        };
        Navigation.InsertPageBefore(EP, Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private void Visible()
    {
        IsExpand = !IsExpand;
    }
}