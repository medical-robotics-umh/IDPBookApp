using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;
using IDPBookApp.Models;
using IDPBookApp.Pages;
using Plugin.CloudFirestore;

namespace IDPBookApp.ViewModel;

[QueryProperty(nameof(InmTrat),nameof(InmTrat))]
[QueryProperty(nameof(Aux2), nameof(Aux2))]
[QueryProperty(nameof(Aux3), nameof(Aux3))]
public partial class TratDetailViewModel : BaseViewModel
{
    readonly FirebaseConnecty firebaseConnecty;
    private readonly TratViewModel viewModel;
    static INavigation Navigation => Shell.Current.Navigation;
    public TratDetailViewModel(FirebaseConnecty firebaseConnecty)
    {
        this.firebaseConnecty = firebaseConnecty;
        GetAdmins();
    }

    public ObservableCollection<Admin> Admins { get; set; } = [];

    [ObservableProperty]
    Tratamiento inmTrat;

    [ObservableProperty]
    public bool subvsbl = false;
    [ObservableProperty]
    public bool intvsbl = false;
    [ObservableProperty]
    bool aux2;
    [ObservableProperty]
    bool aux3;

    async void GetAdmins()
    {
        await Task.Delay(50);
        Run = true;
        var admins = await FirebaseConnecty.GetAdminsModel(firebaseConnecty.pacInfo.Uid,InmTrat.TId);
        // Hay que modificar el método porque si ingreso desde trat anteriores, deberia descargar las admisnitraciones desde tratamientos y no desde tratActual
        if (admins != null && admins.Count > 0)
        {
            Admins.Clear();
            foreach (var admin in admins.AsEnumerable().Reverse())
            {
                Admins.Add(admin);
            }
        }
        Run = false;
    }

    [RelayCommand]
    async Task NavAdminDetailAsync(Admin admin)
    {
        Run = true;
        if (admin is null)
            return;
        await Shell.Current.GoToAsync($"{nameof(AdminDetailPage)}", true,
            new Dictionary<string, object>
            {
                {"Adminis",admin},
                {"Trat",InmTrat.TId}
            });
        Run = false;
    }

    [RelayCommand]
    async Task GoToNewAdmin()
    {
        await Shell.Current.GoToAsync($"{nameof(NewAdminPage)}", true,
            new Dictionary<string, object>
            {
                {"Contador",Admins.Count},
                {"Trat",InmTrat.TId}
            });
    }

    [RelayCommand]
    async Task FinalITrat()
    {
        bool ans = await Shell.Current.DisplayAlert("¡Aviso!", "¿Confirmas la finalización del tratamiento?", "Si", "No");
        if (ans == true)
        {
            Run = true;
            var fecha = DateTime.Parse(InmTrat.TFecha).ToString("yyyy/MM/dd");
            Console.WriteLine(fecha);
            await CrossCloudFirestore.Current
                             .Instance
                             .Collection("IDPbookDB")
                             .Document(firebaseConnecty.pacInfo.Uid)
                             .Collection("tratamientos")
                             .Document(InmTrat.TId)
                             .SetAsync(InmTrat);
            // ciclo for para guardar las admisnistraciones
            await FirebaseConnecty.ElimData(firebaseConnecty.pacInfo.Uid, "tratActual", InmTrat.TId);
            await Shell.Current.DisplayAlert("Tratamiento finalizado", "Los datos del tratamiento se han archivado exitosamente.", "Ok");
            Run = false;
            var newPage = new TratPage(viewModel)
            {
                BindingContext = new TratViewModel(firebaseConnecty)
            };
            Navigation.InsertPageBefore(newPage, Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
            await Shell.Current.GoToAsync("../..");            
            await Launcher.Default.OpenAsync("https://calendar.google.com/calendar/r/day/"+fecha);
        }
    }

    [RelayCommand]
    async Task EliminarITrat()
    {
        bool ans = await Shell.Current.DisplayAlert("¡Aviso!", "Los datos no se podrán recuperar.\n\n¿Confirmas la eliminación del documento?", "Si", "No");
        if (ans == true)
        {
            Run = true;
            await FirebaseConnecty.ElimData(firebaseConnecty.pacInfo.Uid, "tratamientos", InmTrat.TId);
            await Shell.Current.DisplayAlert("Tratamiento eliminado", "Los datos se han eliminado exitosamente.", "Ok");
            Run = false;
            var newPage = new TratPage(viewModel)
            {
                BindingContext = new TratViewModel(firebaseConnecty)
            };
            Navigation.InsertPageBefore(newPage, Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
            await Shell.Current.GoToAsync("../..");
        }
    }
}
