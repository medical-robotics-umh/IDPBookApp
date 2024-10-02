using IDPBookApp.ViewModel;

namespace IDPBookApp.Pages;

public partial class NewTratPage : ContentPage
{
	public NewTratPage(NewTratViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    private void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
		if(BindingContext is NewTratViewModel vm)
		{
            vm.Ovsbl = true;
            if (vm.TTipo == 0)
            {
                vm.Subvsbl = false;
                vm.Intvsbl = true;
            }
            if (vm.TTipo == 1)
            {
                vm.Intvsbl = false;
                vm.Subvsbl = true;
            }
        }
    }
}