using IDPBookApp.ViewModel;
namespace IDPBookApp.Pages;

public partial class NEpisPage : ContentPage
{
    public NEpisPage(NewDataViewModel newDataViewModel)
    {
        InitializeComponent();
        BindingContext = newDataViewModel;
    }
    private void Lista_uno_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (BindingContext is NewDataViewModel viewModel)
        {
            foreach (var selectedItem in e.PreviousSelection)
            {
                var index = viewModel.Items.IndexOf(selectedItem.ToString());
                if (index >= 0 && index < viewModel.SelectItems.Length)
                {
                    viewModel.SelectItems[index] = false;
                }
            }

            foreach (var selectedItem in e.CurrentSelection)
            {
                var index = viewModel.Items.IndexOf(selectedItem.ToString());
                if (index >= 0 && index < viewModel.SelectItems.Length)
                {
                    viewModel.SelectItems[index] = true;
                }
            }

            // Mostrar en consola el arreglo de estados de selección
            Console.WriteLine("Arreglo de estados de selección:");
            foreach (var isSelected in viewModel.SelectItems)
            {
                Console.WriteLine(isSelected);
            }
        }
    }
}