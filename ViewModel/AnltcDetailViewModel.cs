using CommunityToolkit.Mvvm.ComponentModel;
using IDPBookApp.Models;

namespace IDPBookApp.ViewModel;

[QueryProperty("Analitica","Analitica")]
public partial class AnltcDetailViewModel : BaseViewModel
{
    [ObservableProperty]
    AnaliticaModel analitica;
}
