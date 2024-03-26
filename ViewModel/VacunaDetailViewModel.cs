using CommunityToolkit.Mvvm.ComponentModel;
using IDPBookApp.Models;

namespace IDPBookApp.ViewModel;

[QueryProperty(nameof(Vacuna), nameof(Vacuna))]
public partial class VacunaDetailViewModel : BaseViewModel
{
    [ObservableProperty]
    Vacuna vacuna;
}
