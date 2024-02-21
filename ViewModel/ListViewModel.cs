using IDPBookApp.DataBase;
using System.Diagnostics;

namespace IDPBookApp.ViewModel;

public partial class ListViewModel : BaseViewModel
{
    FirebaseConnecty firebaseConnecty;
    public ListViewModel(FirebaseConnecty firebaseConnecty)
    {        
        this.firebaseConnecty = firebaseConnecty;
        GetEpisodios();
    }

    async void GetEpisodios()
    {
        try
        {
            var episodios = await firebaseConnecty.GetEpisodiosModel("/IDPbookDB/info_pacientes/DARL01/episodios/idEpis");
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
                await Shell.Current.DisplayAlert("Alerta", "No se encontraron pacientes", "Ok");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Alerta", $"No se pudo accerder a la base de datos: {ex.Message}", "Ok");
        }
    }
}
