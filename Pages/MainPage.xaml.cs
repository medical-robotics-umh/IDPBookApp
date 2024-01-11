using IDPBookApp.Models;
using IDPBookApp.ViewModel;

namespace IDPBookApp.Pages;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel MainViewModel)
    {
        InitializeComponent();
        listaIconos.ItemsSource = getIcons();
        BindingContext = MainViewModel;
    }

    private List<IconModel> getIcons()
    {
        return new List<IconModel>()
        {
            new IconModel{Nombre="Información personal",Icono="registro.png"},
            new IconModel{Nombre="Estado de la enfermedad",Icono="estado.png"},
            new IconModel{Nombre="Citas",Icono="calendario.png"},
            new IconModel{Nombre="Documentación",Icono="bibliografia.png"},
            new IconModel{Nombre="Contacto sanitario",Icono="contacto.png"},
            new IconModel{Nombre="Redes de pacientes",Icono="redes_pacientes.png"},
        };
    }

    //HACK Metodo para deshabilitar el gesto/botón atrás propio del dispositivo.
    protected override bool OnBackButtonPressed()
    {
        return true;
    }
}