using Google.Cloud.Firestore;

namespace IDPBookApp.Models;

[FirestoreData]
public class HistoriaModel
{
    [FirestoreProperty]
    public string HId { get; set; }
    [FirestoreProperty]
    public string HTitl { get; set; }
    [FirestoreProperty]
    public string Hfecha { get; set; }
    [FirestoreProperty]
    public string HfDiag { get; set; }
    [FirestoreProperty]
    public int HActivo { get; set; }
    [FirestoreProperty]
    public int HTDiag { get; set; }
    [FirestoreProperty]
    public int HTDiagSub { get; set; }
    [FirestoreProperty]
    public string HTDiagChar { get; set; }
    [FirestoreProperty]
    public string HTDiagSubChar { get; set; }
    [FirestoreProperty]
    public string HAlerg { get; set; }
}
