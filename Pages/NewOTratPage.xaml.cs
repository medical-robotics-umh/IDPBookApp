using IDPBookApp.ViewModel;

namespace IDPBookApp.Pages;

public partial class NewOTratPage : ContentPage
{
	public NewOTratPage(NewOTratViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    private void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        int select = picker.SelectedIndex;
        if (BindingContext is NewOTratViewModel vm)
        {
            vm.Vsbl = false;
            if (select == 1)
            {
                vm.Vsbl = true;
            }
        }
    }
}