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
    INavigation Navigation => Shell.Current.Navigation;
    public NewDataViewModel(FirebaseConnecty firebaseConnecty) 
    { 
        this.firebaseConnecty = firebaseConnecty;
    }

    [RelayCommand]
    async Task Cargar()
    {
        //HACK Metodo para reemplazar el viewmodel y cargar automaticamente la lista de objetos.
        await Navigation.PopAsync();
        var EP = new EpisodiosPage(listViewModel)
        {
            BindingContext = new ListViewModel(firebaseConnecty)
        };
        Navigation.InsertPageBefore(EP, Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
        await Shell.Current.GoToAsync("..");
    }

    [ObservableProperty]
    private bool isExpand;

    [RelayCommand]
    private void Visible()
    {
        IsExpand = !IsExpand;
    }

}