using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IDPBookApp.DataBase;
using IDPBookApp.Models;
using IDPBookApp.Pages;
using Plugin.CloudFirestore;

namespace IDPBookApp.ViewModel;

[QueryProperty(nameof(Tratamiento), nameof(Tratamiento))]
public partial class OTratDetailViewModel : BaseViewModel
{
    readonly FirebaseConnecty firebaseConnecty;
    private readonly OtroTratViewModel viewModel;
    static INavigation Navigation => Shell.Current.Navigation;

    public OTratDetailViewModel(FirebaseConnecty firebaseConnecty)
    {
        this.firebaseConnecty = firebaseConnecty;
    }

    [ObservableProperty]
    OtroTrat tratamiento;

    [ObservableProperty]
    public bool edit = false;

    [RelayCommand]
    private void Editar()
    {
        Edit = !Edit;
    }
    [RelayCommand]
    async Task ActualizarTrat()
    {
        if (Tratamiento.OTNombre == string.Empty || Tratamiento.OTNombre == null)
        {
            await App.Current.MainPage.DisplayAlert("Campo vacio.", "Ingresar nombre del tratamiento antes de guardarlo.", "Ok");
        }
        else
        {
            try
            {
                Run = true;
                var NuevoTrat = new OtroTrat
                {
                    Id = Tratamiento.Id,
                    OTFecha = DateTime.Today.ToShortDateString(),
                    OTNombre = Tratamiento.OTNombre,
                    OTDosis = Tratamiento.OTDosis,
                    OTCad = Tratamiento.OTCad,
                    OTFini = Tratamiento.OTFini,
                    OTFfin = Tratamiento.OTFfin
                };
                
                await CrossCloudFirestore.Current
                                 .Instance
                                 .Collection("IDPbookDB")
                                 .Document(firebaseConnecty.pacInfo.Uid)
                                 .Collection("tratamientos")
                                 .Document(Tratamiento.Id)
                                 .UpdateAsync(NuevoTrat);
                Run = false;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Algo salio mal", ex.Message, "Aceptar");
            }

            var newPage = new OtroTratPage(viewModel)
            {
                BindingContext = new OtroTratViewModel(firebaseConnecty)
            };
            Navigation.InsertPageBefore(newPage, Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
            await Shell.Current.GoToAsync("../..");
        }
    }

    [RelayCommand]
    async Task EliminarTrat()
    {
        bool ans = await App.Current.MainPage.DisplayAlert("¡Aviso!", "Si eliminas el tratamiento, los datos no se podrán recuperar.\n\n¿Confirmas la eliminación del tratamiento?", "Si", "No");
        if (ans == true)
        {
            Run = true;
            await CrossCloudFirestore.Current
                         .Instance
                         .Collection("/IDPbookDB")
                         .Document(firebaseConnecty.pacInfo.Uid)
                         .Collection("tratamientos")
                         .Document(Tratamiento.Id)
                         .DeleteAsync();
            char lastChar = Tratamiento.Id[^1];
            int number = int.Parse(lastChar.ToString());
            if (number != (Contador-1))
            {
                for (int i = number + 1; i < Contador; i++)
                {
                    var id = "OTrat" + i.ToString();
                    var trat = await CrossCloudFirestore.Current
                              .Instance
                              .Collection("/IDPbookDB")
                              .Document(firebaseConnecty.pacInfo.Uid)
                              .Collection("tratamientos")
                              .Document(id)
                              .GetAsync();
                    var doc = trat.ToObject<OtroTrat>();
                    var id2 = "OTrat" + (i - 1).ToString();
                    doc.Id = id2;
                    await CrossCloudFirestore.Current
                              .Instance
                              .Collection("/IDPbookDB")
                              .Document(firebaseConnecty.pacInfo.Uid)
                              .Collection("tratamientos")
                              .Document(id2)
                              .SetAsync(doc);
                }
                var trat2 = "OTrat" + (Contador - 1).ToString();
                await CrossCloudFirestore.Current
                             .Instance
                             .Collection("/IDPbookDB")
                             .Document(firebaseConnecty.pacInfo.Uid)
                             .Collection("tratamientos")
                             .Document(trat2)
                             .DeleteAsync();
            }            
            Run = false;
            var newPage = new OtroTratPage(viewModel)
            {
                BindingContext = new OtroTratViewModel(firebaseConnecty)
            };
            Navigation.InsertPageBefore(newPage, Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
            await Shell.Current.GoToAsync("../..");
        }
    }
}
