using CommunityToolkit.Mvvm.ComponentModel;
using IDPBookApp.Models;
using System.Collections.ObjectModel;

namespace IDPBookApp.ViewModel;

[QueryProperty(nameof(IconModel), nameof(IconModel))]
public partial class DetailPageViewModel : BaseViewModel
{
    public ObservableCollection<IconModel> Icon { get; } = new();
    public DetailPageViewModel()
    {
        Title = "Episodios2";
    }

    [ObservableProperty]
    IconModel iconModel;
}
