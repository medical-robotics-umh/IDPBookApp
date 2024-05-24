using CommunityToolkit.Mvvm.ComponentModel;
using IDPBookApp.Models;

namespace IDPBookApp.ViewModel;

[QueryProperty(nameof(Episodio), nameof(Episodio))]
public partial class DetailPageViewModel : BaseViewModel
{
    public DetailPageViewModel()
    {
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
}
