using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;
using IDPBookApp.Models;
using IDPBookApp.Pages;
using Plugin.CloudFirestore;

namespace IDPBookApp.ViewModel;

public partial class NewAdminViewModel : BaseViewModel
{
    readonly FirebaseConnecty firebaseConnecty;
    private readonly TratDetailViewModel viewModel;
    static INavigation Navigation => Shell.Current.Navigation;
    public NewAdminViewModel(FirebaseConnecty firebaseConnecty)
    {
        this.firebaseConnecty = firebaseConnecty;
        AdFecha = DateTime.Today;
    }

    [ObservableProperty]
    public DateTime adFecha;
    [ObservableProperty]
    public bool ef0;
    [ObservableProperty]
    public bool ef1;
    [ObservableProperty]
    public bool ef2;
    [ObservableProperty]
    public bool ef3;
    [ObservableProperty]
    public bool ef4;
    [ObservableProperty]
    public bool ef5;
    [ObservableProperty]
    public bool ef6;

    [RelayCommand]
    async Task NewAdmin()
    {
        Run = true;
        var id = "Admin" + DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
        Contador++;
        var NuevoTrat = new Admin
        {
            AdId = id,
            AdFecha = AdFecha.ToShortDateString(),
            AdName = "Administración "+Contador.ToString(),
            Ef0 = Ef0,
            Ef1 = Ef1,
            Ef2 = Ef2,
            Ef3 = Ef3,
            Ef4 = Ef4,
            Ef5 = Ef5,
            Ef6 = Ef6,

        };
        await CrossCloudFirestore.Current
                         .Instance
                         .Collection("IDPbookDB")
                         .Document(firebaseConnecty.pacInfo.Uid)
                         .Collection("tratActual")
                         .Document(Trat)
                         .Collection("administraciones")
                         .Document(id)
                         .SetAsync(NuevoTrat);
        Run = false;
        await Shell.Current.GoToAsync("../..");
    }
}
