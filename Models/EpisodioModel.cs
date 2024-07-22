namespace IDPBookApp.Models;

public class EpisodioModel
{
    public string EId {  get; set; }
    public string EName { get; set; }
    public string EFecha { get; set; }
    public int EAtenPrim { get; set; }
    public int EUrgHosp{ get; set; }
    public string EDurac { get; set; }
    public int EIngreso { get; set; }
    public int EFiebre { get; set; }
    public bool[] ESinCata { get; set; }
    public string ESinCataChar { get; set; }
    public bool[] ESinDigest { get; set; }
    public string ESinDigestChar { get; set; }
    public bool[] ESinUri { get; set; }
    public string ESinUriChar { get; set; }
    public bool[] ESinCut { get; set; }
    public string ESinCutChar { get; set; }
    public string EOtroSin { get; set; }
    public int ETrat { get; set; }
    public int ETratAnt { get; set; }
    public string ETratAntibio { get; set; }
    public string ETratDias { get; set; }
    public string ETratOtros { get; set; }
}