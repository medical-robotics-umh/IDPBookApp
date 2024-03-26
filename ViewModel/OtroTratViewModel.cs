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

    public ObservableCollection<OtroTrat> OtrosTratmnts { get; set; } = new();

    async void GetOtrosTrat()
    {
        var otrosTratmnts = await FirebaseConnecty.GetOtrosTratModel(firebaseConnecty.pacInfo.Uid);
        if(otrosTratmnts != null && otrosTratmnts.Count > 0)
        {
            OtrosTratmnts.Clear();
            foreach (var tratamiento in otrosTratmnts)
            {
                OtrosTratmnts.Add(tratamiento);
            }
        }
    }

    [RelayCommand]
    async Task GoToNewOtroTratPage()
    {
        await Shell.Current.GoToAsync($"{nameof(NewOTratPage)}?Contador={OtrosTratmnts.Count}");
    }

    [RelayCommand]
    async Task NavOtrTratDtailAsync(OtroTrat tratamiento)
    {
        if (tratamiento is null)
            return;

        await Shell.Current.GoToAsync($"{nameof(OtroTratDetailPage)}", true,
            new Dictionary<string, object>
            {
                {"Tratamiento",tratamiento}
            });
    }
}
