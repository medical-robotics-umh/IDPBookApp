using IDPBookApp.Models;
using IDPBookApp.ViewModel;

namespace IDPBookApp.Pages;

public partial class DetailPage : ContentPage
{
	public DetailPage(DetailPageViewModel detailPageViewModel)
	{
		InitializeComponent();
        listaIconos.ItemsSource = getIcons2();
        listaItems.ItemsSource = getItems();
		BindingContext = detailPageViewModel;
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }

    private List<IconModel> getIcons2()
    {
        return new List<IconModel>()
        {
            new IconModel{Nombre="Historial",Icono="ic_logo_historial_medico.png"},
            new IconModel{Nombre="Agregar episodio",Icono="ic_logo_agregar.png"},
        };
    }

    private List<ItemModel> getItems()
    {
        return new List<ItemModel>()
        {
            new ItemModel{Fecha="05/12/23",Icono="ic_episode_list.png",Episodio="Episodio 8 - "},
            new ItemModel{Fecha="20/10/23",Icono="ic_episode_list.png",Episodio="Episodio 7 - "},
            new ItemModel{Fecha="30/11/23",Icono="ic_episode_list.png",Episodio="Episodio 6 - "},
            new ItemModel{Fecha="25/11/23",Icono="ic_episode_list.png",Episodio="Episodio 5 - "},
            new ItemModel{Fecha="11/10/23",Icono="ic_episode_list.png",Episodio="Episodio 4 - "},
            new ItemModel{Fecha="2/10/23",Icono="ic_episode_list.png",Episodio="Episodio 3 - "},
            new ItemModel{Fecha="18/08/23",Icono="ic_episode_list.png",Episodio="Episodio 2 - "},
            new ItemModel{Fecha="20/05/23",Icono="ic_episode_list.png",Episodio="Episodio 1 - "},
        };
    }
}