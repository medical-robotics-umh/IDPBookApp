using IDPBookApp.ViewModel;

namespace IDPBookApp.Pages;

public partial class NHistorial : ContentPage
{
	public NHistorial(NewHistoViewModel newHisto)
	{
		InitializeComponent();
		BindingContext = newHisto;
	}

    private void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (BindingContext is NewHistoViewModel vm)
        {
            vm.EInf = false;
            vm.EHema = false;
            vm.EADig = false;
            vm.EPulm = false;
            vm.EHepa = false;
            vm.EOnco = false;
            vm.EEndo = false;
            vm.ECardio = false;
            vm.EAuto = false;
            vm.ENeuro = false;
            vm.ECut = false;
            vm.EOtro = false;

            switch (vm.HTDiag)
            {
                case 0:
                    vm.EInf = true;
                    vm.HTDiagSub = EInf.SelectedIndex;
                    if (EInf.SelectedIndex != -1)
                    {
                        vm.HTitl = EInf.SelectedItem.ToString();
                    }                    
                    break;
                case 1:
                    vm.EHema = true;
                    vm.HTDiagSub = EHema.SelectedIndex;
                    if (EHema.SelectedIndex != -1)
                    {
                        vm.HTitl = EHema.SelectedItem.ToString();
                    }
                    break;
                case 2:
                    vm.EADig = true;
                    vm.HTDiagSub = EADig.SelectedIndex;
                    if (EADig.SelectedIndex != -1)
                    {
                        vm.HTitl = EADig.SelectedItem.ToString();
                    }
                    break;
                case 3:
                    vm.EPulm = true;
                    vm.HTDiagSub = EPulm.SelectedIndex;
                    if (EPulm.SelectedIndex != -1)
                    {
                        vm.HTitl = EPulm.SelectedItem.ToString();
                    }
                    break;
                case 4:
                    vm.EHepa = true;
                    vm.HTDiagSub = EHepa.SelectedIndex;
                    if (EHepa.SelectedIndex != -1)
                    {
                        vm.HTitl = EHepa.SelectedItem.ToString();
                    }
                    break;
                case 5:
                    vm.EOnco = true;
                    vm.HTDiagSub = EOnco.SelectedIndex;
                    if (EOnco.SelectedIndex != -1)
                    {
                        vm.HTitl = EOnco.SelectedItem.ToString();
                    }
                    break;
                case 6:
                    vm.EEndo = true;
                    vm.HTDiagSub = EEndo.SelectedIndex;
                    if (EEndo.SelectedIndex != -1)
                    {
                        vm.HTitl = EEndo.SelectedItem.ToString();
                    }
                    break;
                case 7:
                    vm.ECardio = true;
                    vm.HTDiagSub = ECardio.SelectedIndex;
                    if (ECardio.SelectedIndex != -1)
                    {
                        vm.HTitl = ECardio.SelectedItem.ToString();
                    }
                    break;
                case 8:
                    vm.EAuto = true;
                    vm.HTDiagSub = EAuto.SelectedIndex;
                    if (EAuto.SelectedIndex != -1)
                    {
                        vm.HTitl = EAuto.SelectedItem.ToString();
                    }
                    break;
                case 9:
                    vm.ENeuro = true;
                    vm.HTDiagSub = ENeuro.SelectedIndex;
                    if (ENeuro.SelectedIndex != -1)
                    {
                        vm.HTitl = ENeuro.SelectedItem.ToString();
                    }
                    break;
                case 10:
                    vm.ECut = true;
                    vm.HTDiagSub = ECut.SelectedIndex;
                    if (ECut.SelectedIndex != -1)
                    {
                        vm.HTitl = ECut.SelectedItem.ToString();
                    }
                    break;
                case 11:
                    vm.EOtro = true;
                    break;
            }
        }
    }

    private void Picker_SelectedIndexChanged_1(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        int select = picker.SelectedIndex;
        object item = picker.SelectedItem;
        if (BindingContext is NewHistoViewModel vm)
        {
            vm.EOtro = false;
            vm.HTDiagSub = select;
            vm.HTitl = item.ToString();
            if (item.ToString() == "Otro")
            {
                vm.EOtro = true;
            }
        }
    }
}