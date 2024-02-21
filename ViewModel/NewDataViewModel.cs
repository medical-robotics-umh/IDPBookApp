using CommunityToolkit.Mvvm.Input;

namespace IDPBookApp.ViewModel;

public partial class NewDataViewModel : BaseViewModel
{
    public NewDataViewModel() 
    { 
    }

    [RelayCommand]
    async static Task Cargar()
    {
        await Shell.Current.GoToAsync("..");
    }
}