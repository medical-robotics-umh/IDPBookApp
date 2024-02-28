using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;
using IDPBookApp.Pages;
using System.Collections.ObjectModel;

namespace IDPBookApp.ViewModel;

public partial class NewDataViewModel : BaseViewModel
{
    readonly FirebaseConnecty firebaseConnecty;
    readonly ListViewModel listViewModel;

    public ObservableCollection<string> Items { get; set; }

    //public ObservableCollection<bool> SelectedItems { get; set; } = new ObservableCollection<bool>();

    [ObservableProperty]
    private bool isExpand;

    [ObservableProperty]
    private bool[] selectItems;
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
        //SelectedItems = new ObservableCollection<bool>(Enumerable.Repeat(false, Items.Count));
    }

    [RelayCommand]
    async Task Cargar()
    {
        //////////////////////////////////////////////////////////////////////////
        bool[] bools = GetSelectedItemsArray();
        Console.WriteLine("Arreglo de elementos seleccionados:");
        foreach (bool item in bools)
        {
            Console.WriteLine(item);
        }
        //////////////////////////////////////////////////////////////////////////

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

    [RelayCommand]
    private void ItemSelected(string item)
    {
        int index = Items.IndexOf(item);
        SelectItems[index] = !SelectItems[index];
    }

    public bool[] GetSelectedItemsArray()
    {
        return SelectItems;
    }
    //[RelayCommand]
    //private void ToggleSelection(object selectedItem)
    //{
    //    if (selectedItem is string item)
    //    {
    //        int index = Items.IndexOf(item);
    //        if (index >= 0 && index < SelectedItems.Count)
    //        {
    //            SelectedItems[index] = !SelectedItems[index];
    //        }
    //    }
    //}

    //// Método para obtener el arreglo de booleanos
    //public bool[] GetSelectedItemsArray()
    //{
    //    return SelectedItems.Select(item => (bool)item).ToArray();
    //}
}