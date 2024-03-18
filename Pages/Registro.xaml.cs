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

    private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (BindingContext is NewPacViewModel vm)
        {
            if(e.Value==true) 
            { 
                vm.Disable = false;
            }
            else
            {
                vm.Disable = true;
            }
        }
    }

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (BindingContext is NewPacViewModel vm)
        {
            foreach (var selectedItem in e.PreviousSelection)
            {
                var index = vm.ListaDiagcs.IndexOf(selectedItem.ToString());
                if (index >= 0 && index < vm.DiagSelec.Length)
                {
                    vm.DiagSelec[index] = false;
                }
            }

            foreach (var selectedItem in e.CurrentSelection)
            {
                var index = vm.ListaDiagcs.IndexOf(selectedItem.ToString());
                if (index >= 0 && index < vm.DiagSelec.Length)
                {
                    vm.DiagSelec[index] = true;
                }
            }
                        
            Console.WriteLine("Arreglo de estados de selección:");
            foreach (var isSelected in vm.DiagSelec)
            {
                Console.WriteLine(isSelected);
            }
        }
    }
}