using IDPBookApp.Models;
using IDPBookApp.ViewModel;

namespace IDPBookApp.Pages;

public partial class Estado : ContentPage
{
	public Estado(MainViewModel iconViewModel)
	{
		InitializeComponent();
        listaIconos.ItemsSource = getIcons();
        BindingContext = iconViewModel;
    }

    private List<IconModel> getIcons()
    {
        return new List<IconModel>()
        {
            new IconModel{Nombre="Episodios",Icono="ic_logo_episodios.png"},
            new IconModel{Nombre="Pruebas de laboratorio",Icono="ic_logo_laboratorio.png"},
            new IconModel{Nombre="Tratamiento con Inmunoglobulinas",Icono="ic_logo_tratamiento_inmunoglobulinas.png"},
            new IconModel{Nombre="Otros tratamientos",Icono="ic_logo_otros_tratamientos.png"},
            new IconModel{Nombre="Vacunación",Icono="ic_logo_vacunacion.png"},
            new IconModel{Nombre="Encuesta calidad de vida",Icono="ic_logo_calidad_vida.png"},
        };
    }
}