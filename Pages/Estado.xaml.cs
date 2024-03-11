using IDPBookApp.Models;
using IDPBookApp.ViewModel;

namespace IDPBookApp.Pages;

public partial class Estado : ContentPage
{
	public Estado(MainViewModel iconViewModel)
	{
		InitializeComponent();
        BindingContext = iconViewModel;
    }
}