using CommunityToolkit.Mvvm.Input;
using IDPBookApp.Models;
using IDPBookApp.Pages;
using System.Collections.ObjectModel;

namespace IDPBookApp.ViewModel
{
    public partial class IconViewModel : BaseViewModel
    {
        public ObservableCollection<IconModel> Icon { get; } = new();
        public IconViewModel() 
        {
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
        Task SingOut() => Shell.Current.GoToAsync("..");
    }
}
