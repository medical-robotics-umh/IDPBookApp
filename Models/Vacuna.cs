using Google.Cloud.Firestore;

namespace IDPBookApp.Models;

[FirestoreData]
public class Vacuna
{
    [FirestoreProperty]
    public string VId { get; set; }
    [FirestoreProperty]
    public string VNmbre { get; set; }
    [FirestoreProperty]
    public string VFecha { get; set; }
    [FirestoreProperty]
    public string VDosis { get; set; }
    [FirestoreProperty]
    public string VAnoUD { get; set; }
}
