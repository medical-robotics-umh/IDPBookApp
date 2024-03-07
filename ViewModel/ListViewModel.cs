using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;
using IDPBookApp.Models;
using IDPBookApp.Pages;
using System.Diagnostics;

namespace IDPBookApp.ViewModel;

public partial class ListViewModel : BaseViewModel
{
    readonly FirebaseConnecty firebaseConnecty;
    public ListViewModel(FirebaseConnecty firebaseConnecty)
    {        
        this.firebaseConnecty = firebaseConnecty;
        GetEpisodios();
    }
    async void GetEpisodios()
    {
        try
        {
            var episodios = await firebaseConnecty.GetEpisodiosModel("/IDPbookDB/"+firebaseConnecty.userInfo.Uid+"/episodios");
            if (episodios != null && episodios.Count > 0)
            {
                Episodios.Clear();
                foreach (var episodio in episodios)
                {
                    Episodios.Add(episodio);
                }                
            }
            else
            {
                //await Shell.Current.DisplayAlert("Alerta", "No se encontraron pacientes", "Ok");
                //Crear una propiedad string y enlazarla a la propiedad EmptyView del CollectionView para que muestre un mensaje cuando no haya elementos en la base de datos
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Alerta", $"No se pudo accerder a la base de datos: {ex.Message}", "Ok");
        }
    }

    [RelayCommand]
    async Task Navegar_EpisodioAsync(EpisodioModel episodio)
    {
        //if (episodio != null)
        //    return;
        await Shell.Current.GoToAsync(nameof(EpisodioViewPage),true,
            new Dictionary<string,object>
            {
                ["Episodio"] = episodio
            });
    }

    [RelayCommand]
    async Task NavegarNEpi()
    {
        await Shell.Current.GoToAsync($"{nameof(NEpisPage)}?Contador={Episodios.Count}");
    }
}
