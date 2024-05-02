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

    private void Lista_SinCat_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (BindingContext is NewDataViewModel viewModel)
        {
            foreach (var selectedItem in e.PreviousSelection)
            {
                var index = viewModel.ListaSinCat.IndexOf(selectedItem.ToString());
                if (index >= 0 && index < viewModel.SelectSinCat.Length)
                {
                    viewModel.SelectSinCat[index] = false;
                }
            }

            foreach (var selectedItem in e.CurrentSelection)
            {
                var index = viewModel.ListaSinCat.IndexOf(selectedItem.ToString());
                if (index >= 0 && index < viewModel.SelectSinCat.Length)
                {
                    viewModel.SelectSinCat[index] = true;
                }
            }
            // Mostrar en consola el arreglo de estados de selección
            //Console.WriteLine("Arreglo de estados de selección:");
            //foreach (var isSelected in viewModel.SelectSinCat)
            //{
            //    Console.WriteLine(isSelected);
            //}
        }
    }

    private void Lista_SinDigest_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (BindingContext is NewDataViewModel viewModel)
        {
            foreach (var selectedItem in e.PreviousSelection)
            {
                var index = viewModel.ListaSinDigest.IndexOf(selectedItem.ToString());
                if (index >= 0 && index < viewModel.SelectSinDigest.Length)
                {
                    viewModel.SelectSinDigest[index] = false;
                }
            }

            foreach (var selectedItem in e.CurrentSelection)
            {
                var index = viewModel.ListaSinDigest.IndexOf(selectedItem.ToString());
                if (index >= 0 && index < viewModel.SelectSinDigest.Length)
                {
                    viewModel.SelectSinDigest[index] = true;
                }
            }
        }
    }

    private void Lista_SinUri_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (BindingContext is NewDataViewModel viewModel)
        {
            foreach (var selectedItem in e.PreviousSelection)
            {
                var index = viewModel.ListaSinUri.IndexOf(selectedItem.ToString());
                if (index >= 0 && index < viewModel.SelectSinUri.Length)
                {
                    viewModel.SelectSinUri[index] = false;
                }
            }

            foreach (var selectedItem in e.CurrentSelection)
            {
                var index = viewModel.ListaSinUri.IndexOf(selectedItem.ToString());
                if (index >= 0 && index < viewModel.SelectSinUri.Length)
                {
                    viewModel.SelectSinUri[index] = true;
                }
            }
        }
    }

    private void Lista_SinCut_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (BindingContext is NewDataViewModel viewModel)
        {
            foreach (var selectedItem in e.PreviousSelection)
            {
                var index = viewModel.ListaSinCut.IndexOf(selectedItem.ToString());
                if (index >= 0 && index < viewModel.SelectSinCut.Length)
                {
                    viewModel.SelectSinCut[index] = false;
                }
            }

            foreach (var selectedItem in e.CurrentSelection)
            {
                var index = viewModel.ListaSinCut.IndexOf(selectedItem.ToString());
                if (index >= 0 && index < viewModel.SelectSinCut.Length)
                {
                    viewModel.SelectSinCut[index] = true;
                }
            }
        }
    }

    private void Switch_Toggled(object sender, ToggledEventArgs e)
    {
        if (BindingContext is NewDataViewModel viewModel)
        {
            viewModel.Trat_visbl = !viewModel.Trat_visbl;
        }
    }
}