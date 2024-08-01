namespace IDPBookApp.Pages;

using IDPBookApp.ViewModel;

public partial class Registro : ContentPage
{
	public Registro(NewPacViewModel newPac)
	{
		InitializeComponent();
		BindingContext = newPac;
    }

    private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
    {
        if (BindingContext is NewPacViewModel vm)
        {
            int añosTranscurridos = DateTime.Today.Year - vm.FNac.Year;
            if (DateTime.Today.Month < vm.FNac.Month || (DateTime.Today.Month == vm.FNac.Month && DateTime.Today.Day < vm.FNac.Day))
            {
                añosTranscurridos--; // Resta un año si el cumpleaños aún no ha ocurrido este año
            }
            vm.EdadPac = añosTranscurridos;
        }
    }

    private void CheckBox_CheckedChanged0(object sender, CheckedChangedEventArgs e)
    {
        if (BindingContext is NewPacViewModel vm)
        {
            vm.DiagPpal = -1;
            vm.SexG = null;
            vm.PacVsbl = e.Value;
            check1.IsChecked = !e.Value;
        }
    }
    private void CheckBox_CheckedChanged1(object sender, CheckedChangedEventArgs e)
    {
        if (BindingContext is NewPacViewModel vm)
        {
            vm.DiagPpal = -1;
            vm.SexG = null;
            vm.PacVsbl = !e.Value;
        }
    }

    private void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        int select = picker.SelectedIndex;
        if (BindingContext is NewPacViewModel vm)
        {
            vm.PacVsbl1 = true;
            if (select == 13)
            {
                vm.PacVsbl1 = false;
            }
        }
    }
}