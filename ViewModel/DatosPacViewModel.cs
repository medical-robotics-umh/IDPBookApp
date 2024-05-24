using CommunityToolkit.Mvvm.ComponentModel;
using IDPBookApp.DataBase;
using System.Globalization;

namespace IDPBookApp.ViewModel;
public partial class DatosPacViewModel : BaseViewModel
{
    readonly FirebaseConnecty firebaseConnecty;
    public DatosPacViewModel(FirebaseConnecty firebaseConnecty)
    {
        this.firebaseConnecty = firebaseConnecty;
        GetPac();
    }

    [ObservableProperty]
    int fecha;

    [ObservableProperty]
    public bool dICV;
    [ObservableProperty]
    public bool dAgam;
    [ObservableProperty]
    public bool dDIgA;
    [ObservableProperty]
    public bool dDSI;
    [ObservableProperty]
    public bool dDRAE;
    [ObservableProperty]
    public bool dIComb;
    [ObservableProperty]
    public bool dEGC;
    [ObservableProperty]
    public bool dM22q;
    [ObservableProperty]
    public bool dSWA;
    [ObservableProperty]
    public bool dSHIgE;
    [ObservableProperty]
    public bool dSHIgM;
    [ObservableProperty]
    public bool dALPS;
    [ObservableProperty]
    public bool dCMC;

    async void GetPac()
    {
        Paciente = await FirebaseConnecty.GetPacienteModel(firebaseConnecty.pacInfo.Uid);
        if(Paciente != null)
        {
            var nac = DateTime.ParseExact(Paciente.FechNac, "d/m/yyyy", CultureInfo.InvariantCulture);
            int añosTranscurridos = DateTime.Today.Year - nac.Year;
            if (DateTime.Today.Month < nac.Month || (DateTime.Today.Month == nac.Month && DateTime.Today.Day < nac.Day))
            {
                añosTranscurridos--; //Resta un año si el cumpleaños aún no ha ocurrido este año
            }
            Fecha = añosTranscurridos;

            DICV = Paciente.Diagnsc[0];
            DAgam = Paciente.Diagnsc[1];
            DDIgA = Paciente.Diagnsc[2];
            DDSI = Paciente.Diagnsc[3];
            DDRAE = Paciente.Diagnsc[4];
            DIComb = Paciente.Diagnsc[5];
            DEGC = Paciente.Diagnsc[6];
            DM22q = Paciente.Diagnsc[7];
            DSWA = Paciente.Diagnsc[8];
            DSHIgE = Paciente.Diagnsc[9];
            DSHIgM = Paciente.Diagnsc[10];
            DALPS = Paciente.Diagnsc[11];
            DCMC = Paciente.Diagnsc[12];
        }        
    }
}
