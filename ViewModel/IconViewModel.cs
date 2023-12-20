using CommunityToolkit.Mvvm.Input;
using IDPBookApp.Models;
using IDPBookApp.Pages;
using System.Collections.ObjectModel;
using IDPBookApp.DataBase;

namespace IDPBookApp.ViewModel
{
    public partial class IconViewModel : BaseViewModel
    {
        FirebaseConnecty firebaseConnecty;
        public ObservableCollection<IconModel> Icon { get; } = new();
        public IconViewModel(FirebaseConnecty firebaseConnecty) 
        {
            this.firebaseConnecty = firebaseConnecty;
            Title = "IDPBook";
        }
        [RelayCommand]
        async Task Navegar(IconModel icon)
        {
            await Shell.Current.GoToAsync($"{nameof(DetailPage)}", true,
                new Dictionary<string, object>
                {
                    {"Icono", icon}
                });
        }

        [RelayCommand]
        async Task LogOutBtn()
        {
            firebaseConnecty.LogOut();
            await App.Current.MainPage.DisplayAlert("Aviso", "Sesión finalizada correctamente", "Ok");
            await Shell.Current.GoToAsync("..");
        }

    }
}
