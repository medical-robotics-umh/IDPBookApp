using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;
using IDPBookApp.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace IDPBookApp.ViewModel
{
    public partial class MainViewModel : BaseViewModel
    {
        FirebaseConnecty firebaseConnecty;
        public ObservableCollection<EpisodioModel> Episodios { get; set; } = new ObservableCollection<EpisodioModel>();
        public MainViewModel(FirebaseConnecty firebaseConnecty) 
        {
            this.firebaseConnecty = firebaseConnecty;
            Title = "IDPBook";
            GetEpisodios();
        }

        [RelayCommand]
        async static Task Navegar(string ruta)
        {
            await Shell.Current.GoToAsync(ruta);
        }

        [RelayCommand]
        async Task LogOutBtn()
        {
            firebaseConnecty.LogOut();
            await App.Current.MainPage.DisplayAlert("Aviso", "Sesión finalizada correctamente", "Ok");
            await Shell.Current.GoToAsync("///LoginPage");
        }

        [RelayCommand]
        async Task BackBtn()
        {
            await Shell.Current.GoToAsync("..");
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
}
