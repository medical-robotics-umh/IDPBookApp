using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;
using IDPBookApp.Models;
using IDPBookApp.Pages;
using System.Collections.ObjectModel;

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
        var ruta = "tratActual";
        if (Aux3 == true)
            ruta = "tratamientos";
        var admins = await firebaseConnecty.GetAdminsModel(firebaseConnecty.pacInfo.Uid,InmTrat.TId,ruta);
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
                {"Trat",InmTrat.TId},
                {"Elimvsbl", Aux2}
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
            ////////////////////////////////////////////COPIA DE ADMINISTRACIONES//////////////////////////////////////////////
            Run = true;
            var fecha = DateTime.Parse(InmTrat.TFecha).ToString("yyyy/MM/dd");
            await firebaseConnecty.SaveData(firebaseConnecty.pacInfo.Uid,"tratamientos", InmTrat.TId, InmTrat);
            var administracionesSnapshot = await firebaseConnecty.GetAdminsModel(firebaseConnecty.pacInfo.Uid, InmTrat.TId, "tratActual");
            foreach (var adminDoc in administracionesSnapshot)
            {
                var adminData = adminDoc;
                await firebaseConnecty.SaveData(firebaseConnecty.pacInfo.Uid, "tratamientos/" + InmTrat.TId + "/administraciones", adminDoc.AdId, adminData);
            }
            await firebaseConnecty.EliminarTrat(firebaseConnecty.pacInfo.Uid, "tratActual", InmTrat.TId);
            Run = false;
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            var newPage = new TratPage(viewModel)
            {
                BindingContext = new TratViewModel(firebaseConnecty)
            };
            Navigation.InsertPageBefore(newPage, Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
            await Shell.Current.GoToAsync("../..");
            await Shell.Current.DisplayAlert("Tratamiento finalizado", "Los datos del tratamiento se han archivado exitosamente. \n\nEl calendario se abrirá para eliminar los recordatorios. Selecciona el evento y elige 'Todos los eventos' para eliminar todos los recordatorios.", "Ok");
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
            await firebaseConnecty.EliminarTrat(firebaseConnecty.pacInfo.Uid, "tratamientos", InmTrat.TId);
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
