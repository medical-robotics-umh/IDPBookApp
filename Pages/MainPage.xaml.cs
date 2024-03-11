using IDPBookApp.Models;
using IDPBookApp.ViewModel;

namespace IDPBookApp.Pages;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel MainViewModel)
    {
        InitializeComponent();
        BindingContext = MainViewModel;
    }

    //HACK Metodo para deshabilitar el gesto/botón atrás propio del dispositivo.
    protected override bool OnBackButtonPressed()
    {
        return true;
    }
}