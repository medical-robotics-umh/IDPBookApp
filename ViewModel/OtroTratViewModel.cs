using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;
using IDPBookApp.Models;
using IDPBookApp.Pages;
using System.Collections.ObjectModel;

namespace IDPBookApp.ViewModel;

public partial class OtroTratViewModel: BaseViewModel
{
    readonly FirebaseConnecty firebaseConnecty;
    public OtroTratViewModel(FirebaseConnecty firebaseConnecty)
    {
        this.firebaseConnecty = firebaseConnecty;
        GetOtrosTrat();
    }

    public ObservableCollection<OtroTrat> OtrosTratmnts { get; set; } = [];

    async void GetOtrosTrat()
    {
        Run = true;
        var otrosTratmnts = await FirebaseConnecty.GetOtrosTratModel(firebaseConnecty.pacInfo.Uid);
        if(otrosTratmnts != null && otrosTratmnts.Count > 0)
        {
            OtrosTratmnts.Clear();
            foreach (var tratamiento in otrosTratmnts.AsEnumerable().Reverse())
            {
                OtrosTratmnts.Add(tratamiento);
            }
        }
        Run = false;
    }

    [RelayCommand]
    async Task NavOtrTratDtailAsync(OtroTrat tratamiento)
    {
        Run = true;
        if (tratamiento is null)
            return;

        await Shell.Current.GoToAsync($"{nameof(OtroTratDetailPage)}", true,
            new Dictionary<string, object>
            {
                {"Tratamiento",tratamiento}
            });
        Run = false;
    }
}
