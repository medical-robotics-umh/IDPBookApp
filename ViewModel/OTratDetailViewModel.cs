using CommunityToolkit.Mvvm.ComponentModel;
using IDPBookApp.Models;

namespace IDPBookApp.ViewModel;

[QueryProperty(nameof(Tratamiento), nameof(Tratamiento))]
public partial class OTratDetailViewModel : BaseViewModel
{  
    [ObservableProperty]
    OtroTrat tratamiento;
}
