using IDPBookApp.ViewModel;

namespace IDPBookApp.Pages;

public partial class NuevaEncuestaPage : ContentPage
{
    public NuevaEncuestaPage(NuevaEncuestaViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
    protected override bool OnBackButtonPressed()
    {
        if (BindingContext is NuevaEncuestaViewModel vm)
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
    private void OnNavigating(object sender, ShellNavigatingEventArgs e)
    {
        if (BindingContext is NuevaEncuestaViewModel vm)
        {
            if (vm.ValidCuest == false)
            {
                if (e.Source == ShellNavigationSource.Pop) // Detecta si es un retroceso
                {
                    e.Cancel(); // Cancela el retroceso temporalmente
                }
            }
        }
    }
#endif
}