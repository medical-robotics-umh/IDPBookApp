using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;
using IDPBookApp.Models;
using IDPBookApp.Pages;
using System.Collections.ObjectModel;

namespace IDPBookApp.ViewModel;

public partial class EncuestasViewModel : BaseViewModel
{
    readonly FirebaseConnecty firebaseConnecty;

    public EncuestasViewModel(FirebaseConnecty firebaseConnecty)
    {
        this.firebaseConnecty = firebaseConnecty;
        GetCuest();
    }
    public ObservableCollection<Cuestionario> Cuestionarios { get; set; } = new();

    async void GetCuest()
    {
        Run = true;
        var cuestionarios = await FirebaseConnecty.GetCuestionariosModel(firebaseConnecty.pacInfo.Uid);
        if (cuestionarios != null && cuestionarios.Count > 0)
        {
            Cuestionarios.Clear();
            foreach (var cuest in cuestionarios)
            {
                Cuestionarios.Add(cuest);
            }
        }
        if (cuestionarios.Count >= 2)
            Btn_visbl = true;
        Run = false;
    }

    [RelayCommand]
    async Task GoToNewCuest()
    {
        await Shell.Current.GoToAsync($"{nameof(NuevaEncuestaPage)}", true,
            new Dictionary<string, object>
            {
                {"Contador",Cuestionarios.Count},
                {"ValidCuest",true}
            });
    }

    [RelayCommand]
    async Task NavCuestDtailAsync(Cuestionario cuestionario)
    {
        Run = true;
        if (cuestionario is null)
            return;

        await Shell.Current.GoToAsync($"{nameof(EncuestaDetailPage)}", true,
            new Dictionary<string, object>
            {
                {"Contador",Cuestionarios.Count},
                {"Cuestionario",cuestionario}
            });
        Run = false;
    }
}
