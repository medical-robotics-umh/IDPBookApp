using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;
using IDPBookApp.Models;
using System.Collections.ObjectModel;

namespace IDPBookApp.ViewModel
{
    public partial class MainViewModel : BaseViewModel
    {
        FirebaseConnecty firebaseConnecty;
        public ObservableCollection<IconModel> Icon { get; } = new();
        public MainViewModel(FirebaseConnecty firebaseConnecty) 
        {
            this.firebaseConnecty = firebaseConnecty;
            Title = "IDPBook";
        }

        [RelayCommand]
        async static Task Navegar(string ruta)
        {
            await Shell.Current.GoToAsync(ruta);
        }

        [RelayCommand]
        async Task LogOutBtn()
        {
            firebaseConnecty.LogOut();
            await App.Current.MainPage.DisplayAlert("Aviso", "Sesión finalizada correctamente", "Ok");
            await Shell.Current.GoToAsync("///LoginPage");
        }

        [RelayCommand]
        async Task BackBtn()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
