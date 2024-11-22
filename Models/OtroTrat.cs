using Google.Cloud.Firestore;
namespace IDPBookApp.Models;
[FirestoreData]
public class OtroTrat
{
    [FirestoreProperty]
    public string Id { get; set; }
    [FirestoreProperty]
    public string OTFecha { get; set; }
    [FirestoreProperty]
    public string OTNombre { get; set; }
    [FirestoreProperty]
    public string OTDosis { get; set; }
    [FirestoreProperty]
    public string OTCad { get; set; }    
    [FirestoreProperty]
    public string OTFini { get; set; }
    [FirestoreProperty]
    public string OTFfin { get; set; }
    [FirestoreProperty]
    public int OTCronc { get; set; }
}
