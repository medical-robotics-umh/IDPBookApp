using IDPBookApp.ViewModel;
using Plugin.CloudFirestore;

namespace IDPBookApp.Pages;

public partial class CambiarPass : ContentPage
{
    public CambiarPass(CamPassViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    private async void VerifcarCorreo(object sender, EventArgs e)
    {
        if (correo.Text != string.Empty)
        {
            var query = await CrossCloudFirestore.Current
                     .Instance
                     .Collection("/IDPbookDB")
                     .WhereEqualsTo("Correo", correo.Text)
                     .GetAsync();
            if (query.Count == 0)
            {
                await App.Current.MainPage.DisplayAlert("Usuario no encontrado.", $"No existe un usuario registrado con el correo:\n\n" + correo.Text + "\n\nComprueba el correo o comunicate con el personal médico.", "Ok");
            }
        }
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