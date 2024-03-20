using IDPBookApp.DataBase;
using IDPBookApp.Models;
using IDPBookApp.Pages;
using System.Collections.ObjectModel;

namespace IDPBookApp.ViewModel;

public partial class NewHistoViewModel : BaseViewModel
{
    readonly FirebaseConnecty firebaseConnecty;
    public NewHistoViewModel(FirebaseConnecty firebaseConnecty)
    {

        this.firebaseConnecty = firebaseConnecty;

    }
}
