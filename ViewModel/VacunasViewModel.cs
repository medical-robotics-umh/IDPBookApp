using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;
using IDPBookApp.Models;
using IDPBookApp.Pages;
using System.Collections.ObjectModel;

namespace IDPBookApp.ViewModel;

public partial class VacunasViewModel : BaseViewModel
{
    readonly FirebaseConnecty firebaseConnecty;
    public VacunasViewModel(FirebaseConnecty firebaseConnecty)
    {
        this.firebaseConnecty = firebaseConnecty;
        GetVacunas();
    }

    public ObservableCollection<Vacuna> Vacunas { get; set; } = [];

    async void GetVacunas()
    {
        Run = true;
        var vacunas = await firebaseConnecty.GetModelList<Vacuna>(firebaseConnecty.pacInfo.Uid,"vacunas");
        if (vacunas != null && vacunas.Count > 0)
        {
            Vacunas.Clear();
            foreach (var item in vacunas.AsEnumerable().Reverse())
            {
                Vacunas.Add(item);
            }
        }
        Run = false;
    }

    [RelayCommand]
    async Task NavVacunDtailAsync(Vacuna vacuna)
    {
        Run= true;
        if (vacuna is null)
            return;

        await Shell.Current.GoToAsync($"{nameof(VacunaDetailPage)}", true,
            new Dictionary<string, object>
            {
                {"Vacuna",vacuna}
            });
        Run= false;
    }
}
