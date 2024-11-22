using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;
using IDPBookApp.Models;
using IDPBookApp.Pages;
using System.Collections.ObjectModel;

namespace IDPBookApp.ViewModel;

public partial class TratViewModel : BaseViewModel
{
    readonly FirebaseConnecty firebaseConnecty;
    public TratViewModel(FirebaseConnecty firebaseConnecty)
    {
        this.firebaseConnecty = firebaseConnecty;
        GetTrat();
    }

    public ObservableCollection<Tratamiento> Tratamientos { get; set; } = [];
    public ObservableCollection<Tratamiento> TratAct { get; set; } = [];

    [ObservableProperty]
    public bool aux = true;

    async void GetTrat()
    {
        Run = true;
        var tratamientos = await firebaseConnecty.GetModelList<Tratamiento>(firebaseConnecty.pacInfo.Uid, "tratamientos");
        if (tratamientos != null && tratamientos.Count > 0)
        {
            Tratamientos.Clear();
            foreach (var tratamiento in tratamientos.AsEnumerable().Reverse())
            {
                Tratamientos.Add(tratamiento);
            }
        }
        var tratact = await firebaseConnecty.GetModelList<Tratamiento>(firebaseConnecty.pacInfo.Uid, "tratActual");
        if (tratact != null && tratact.Count > 0)
        { 
            TratAct.Clear();
            var ulTrat = tratact.LastOrDefault();
            if (ulTrat != null)
                TratAct.Add(ulTrat);
            Aux = false;
        }
        Run = false;
    }

    [RelayCommand]
    async Task NavTratDetailAsync(Tratamiento tratamiento)
    {
        Run = true;
        if (tratamiento is null)
            return;
        await Shell.Current.GoToAsync($"{nameof(TratDetailPage)}", true,
            new Dictionary<string, object>
            {
                {"InmTrat",tratamiento},
                {"Aux2",true},
                {"Aux3",false}
            });
        Run = false;
    }

    [RelayCommand]
    async Task NavTratDetail2Async(Tratamiento tratamiento)
    {
        Run = true;
        if (tratamiento is null)
            return;
        await Shell.Current.GoToAsync($"{nameof(TratDetailPage)}", true,
            new Dictionary<string, object>
            {
                {"InmTrat",tratamiento},
                {"Aux2",false},
                {"Aux3",true}
            });
        Run = false;
    }
}
