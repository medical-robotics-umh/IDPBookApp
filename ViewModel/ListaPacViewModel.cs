using IDPBookApp.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDPBookApp.ViewModel;

public partial class ListaPacViewModel:BaseViewModel
{
    readonly FirebaseConnecty firebaseConnecty;
    public ListaPacViewModel(FirebaseConnecty firebaseConnecty)
    {
        this.firebaseConnecty = firebaseConnecty;
    }

}
