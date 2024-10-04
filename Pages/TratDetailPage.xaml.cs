using IDPBookApp.ViewModel;

namespace IDPBookApp.Pages;

public partial class TratDetailPage : ContentPage
{
	public TratDetailPage(TratDetailViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    private void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (BindingContext is TratDetailViewModel vm)
        {
            if (vm.InmTrat.TTipo == 0)
            {                
                vm.Intvsbl = true;
            }
            if (vm.InmTrat.TTipo == 1)
            {
                vm.Subvsbl = true;
            }
        }
    }
}