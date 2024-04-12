using CommunityToolkit.Mvvm.ComponentModel;
using IDPBookApp.Models;

namespace IDPBookApp.ViewModel;

[QueryProperty(nameof(Cuestionario),nameof(Cuestionario))]
public partial class EncuestaDetailViewModel : BaseViewModel
{
    [ObservableProperty]
    Cuestionario cuestionario;
}
