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
        if (BindingContext is MainViewModel vm)
        {
            _ = vm.OnBackButtonPressedAsync();
        }
        return true;
    }
#if IOS
    protected override void OnAppearing()
    {
        base.OnAppearing();
        Shell.Current.Navigating += OnNavigating;
    }
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        Shell.Current.Navigating -= OnNavigating;
    }
    private async void OnNavigating(object sender, ShellNavigatingEventArgs e)
    {
        if (e.Source == ShellNavigationSource.Pop) // Detecta si es un retroceso
        {
            e.Cancel(); // Cancela el retroceso temporalmente

            if (BindingContext is MainViewModel vm)
            {
                await vm.OnBackButtonPressedAsync();
            }
        }
    }
#endif
}