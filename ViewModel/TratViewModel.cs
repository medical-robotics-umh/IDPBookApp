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

    async void GetTrat()
    {
        Run = true;
        var tratamientos = await FirebaseConnecty.GetTratInmunoModel(firebaseConnecty.pacInfo.Uid);
        if (tratamientos != null && tratamientos.Count > 0)
        {
            Tratamientos.Clear();
            TratAct.Clear();
            foreach (var tratamiento in tratamientos.AsEnumerable().Reverse().Skip(1))
            {
                Tratamientos.Add(tratamiento);
            }
            var ulTrat = tratamientos.LastOrDefault();
            if (ulTrat != null)
                TratAct.Add(ulTrat);
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
                {"InmTrat",tratamiento}
            });
        Run = false;
    }
}
