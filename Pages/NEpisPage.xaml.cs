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

    private void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        int select = picker.SelectedIndex;
        if (BindingContext is NewDataViewModel vm)
        {
            vm.Trat_visbl = false;
            if (select == 0)
            {
                vm.Trat_visbl = true;
            }            
        }
    }
}