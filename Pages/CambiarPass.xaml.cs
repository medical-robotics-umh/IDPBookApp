using IDPBookApp.ViewModel;

namespace IDPBookApp.Pages;

public partial class CambiarPass : ContentPage
{
    public CambiarPass(CamPassViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (BindingContext is CamPassViewModel vm)
        {
            if (e.Value == true)
            {
                vm.Disable = false;
            }
            else
            {
                vm.Disable = true;
            }
        }
    }
}