using Google.Cloud.Firestore;

namespace IDPBookApp.Models;

[FirestoreData]
public class EpisodioModel
{
    [FirestoreProperty]
    public string EId {  get; set; }
    [FirestoreProperty]
    public string EName { get; set; }
    [FirestoreProperty]
    public string EFecha { get; set; }
    [FirestoreProperty]
    public int EAtenPrim { get; set; }
    [FirestoreProperty]
    public int EUrgHosp{ get; set; }
    [FirestoreProperty]
    public string EDurac { get; set; }
    [FirestoreProperty]
    public int EIngreso { get; set; }
    [FirestoreProperty]
    public int EFiebre { get; set; }
    [FirestoreProperty]
    public bool[] ESinCata { get; set; }
    [FirestoreProperty]
    public string ESinCataChar { get; set; }
    [FirestoreProperty]
    public bool[] ESinDigest { get; set; }
    [FirestoreProperty]
    public string ESinDigestChar { get; set; }
    [FirestoreProperty]
    public bool[] ESinUri { get; set; }
    [FirestoreProperty]
    public string ESinUriChar { get; set; }
    [FirestoreProperty]
    public bool[] ESinCut { get; set; }
    [FirestoreProperty]
    public string ESinCutChar { get; set; }
    [FirestoreProperty]
    public string EOtroSin { get; set; }
    [FirestoreProperty]
    public int ETrat { get; set; }
    [FirestoreProperty]
    public int ETratAnt { get; set; }
    [FirestoreProperty]
    public string ETratAntibio { get; set; }
    [FirestoreProperty]
    public string ETratDias { get; set; }
    [FirestoreProperty]
    public string ETratOtros { get; set; }
}