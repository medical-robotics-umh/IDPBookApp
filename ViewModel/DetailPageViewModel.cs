using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;
using IDPBookApp.Models;
using IDPBookApp.Pages;

namespace IDPBookApp.ViewModel;

[QueryProperty(nameof(Episodio), nameof(Episodio))]
public partial class DetailPageViewModel : BaseViewModel
{
    readonly FirebaseConnecty firebaseConnecty;
    private readonly ListViewModel listViewModel;
    static INavigation Navigation => Shell.Current.Navigation;
    public DetailPageViewModel(FirebaseConnecty firebaseConnecty)
    {
        this.firebaseConnecty = firebaseConnecty;
        _ = SeleSin();
    }

    [ObservableProperty]
    EpisodioModel episodio;

    private async Task SeleSin()
    {
        await Task.Delay(50);

        STos = Episodio.ESinCata[0];
        SMoco = Episodio.ESinCata[1];
        SDGarg = Episodio.ESinCata[2];
        SDTorax = Episodio.ESinCata[3];
        SSAh = Episodio.ESinCata[4];

        SDiar = Episodio.ESinDigest[0];
        SNaVo = Episodio.ESinDigest[1];
        SEstr = Episodio.ESinDigest[2];
        SDAbd = Episodio.ESinDigest[3];

        SEscz = Episodio.ESinUri[0];
        SOOsc = Episodio.ESinUri[1];
        SOMal = Episodio.ESinUri[2];

        SPicor = Episodio.ESinCut[0];
        SDolor = Episodio.ESinCut[1];
        SColR = Episodio.ESinCut[2];
    }

    [RelayCommand]
    async Task ElimHisto()
    {
        bool ans = await Shell.Current.DisplayAlert("¡Aviso!", "Los datos no se podrán recuperar.\n\n¿Confirmas la eliminación del documento?", "Si", "No");
        if (ans == true)
        {
            Run = true;
            await firebaseConnecty.ElimDocs(firebaseConnecty.pacInfo.Uid, "episodios", Episodio.EId);
            await Shell.Current.DisplayAlert("Episodio eliminado", "Los datos se han eliminado exitosamente.", "Ok");
            Run = false;
            var EP = new EpisodiosPage(listViewModel)
            {
                BindingContext = new ListViewModel(firebaseConnecty)
            };
            Navigation.InsertPageBefore(EP, Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
            await Shell.Current.GoToAsync("../..");
        }
    }
}
