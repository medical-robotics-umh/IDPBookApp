using IDPBookApp.ViewModel;
namespace IDPBookApp.Pages;

public partial class NEpisPage : ContentPage
{
    public NEpisPage(NewDataViewModel newDataViewModel)
    {
        InitializeComponent();
        BindingContext = newDataViewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }

    private void Switch_Toggled(object sender, ToggledEventArgs e)
    {
        if (BindingContext is NewDataViewModel viewModel)
        {
            viewModel.Trat_visbl = !viewModel.Trat_visbl;
        }
    }
}