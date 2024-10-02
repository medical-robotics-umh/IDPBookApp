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

namespace IDPBookApp.ViewModel;

[QueryProperty(nameof(InmTrat),nameof(InmTrat))]
public partial class TratDetailViewModel : BaseViewModel
{
    readonly FirebaseConnecty firebaseConnecty;
    private readonly TratViewModel viewModel;
    static INavigation Navigation => Shell.Current.Navigation;
    public TratDetailViewModel(FirebaseConnecty firebaseConnecty)
    {
        this.firebaseConnecty = firebaseConnecty;
        GetTrat();
    }

    [ObservableProperty]
    Tratamiento inmTrat;

    [ObservableProperty]
    public bool subvsbl = false;
    [ObservableProperty]
    public bool intvsbl = false;

    void GetTrat()
    {
        Run = true;
        if (InmTrat != null)
        {
            if (InmTrat.TTipo == 0)
            {
                Intvsbl = true;
            }
            if (InmTrat.TTipo == 1)
            {
                Subvsbl = true;
            }
        }
        Run = false;
    }
}
