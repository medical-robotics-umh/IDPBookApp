using IDPBookApp.Models;
using IDPBookApp.ViewModel;
namespace IDPBookApp.Pages;

public partial class NEpisPage : ContentPage
{
    public NEpisPage(NewDataViewModel newDataViewModel)
    {
        InitializeComponent();
        BindingContext = newDataViewModel;
    }

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var lista = e.CurrentSelection;
        foreach (var item in lista)
        {
            switch (item)
            {
                case "Tos":
                    
                    break;

                default: 
                    break;
            }
        }
    }
}