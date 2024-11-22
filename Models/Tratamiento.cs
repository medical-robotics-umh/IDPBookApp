using Google.Cloud.Firestore;
namespace IDPBookApp.Models;
[FirestoreData]
public class Tratamiento
{
    [FirestoreProperty]
    public string TId { get; set; }
    [FirestoreProperty]
    public string TNom { get; set; }
    [FirestoreProperty]
    public string TFecha { get; set; }
    [FirestoreProperty]
    public string TTs { get; set; }
    [FirestoreProperty]
    public int TTipo { get; set; }
    [FirestoreProperty]
    public int TPrepI { get; set; }
    [FirestoreProperty]
    public int TPrepS { get; set; }
    [FirestoreProperty]
    public int TDosis { get; set; }
    [FirestoreProperty]
    public int TCad { get; set; }
    [FirestoreProperty]
    public int TTimeInf { get; set; }
    [FirestoreProperty]
    public int TVelInf { get; set; }
    [FirestoreProperty]
    public int TVolInf { get; set; }
}
