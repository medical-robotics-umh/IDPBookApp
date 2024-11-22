using Google.Cloud.Firestore;
namespace IDPBookApp.Models;
[FirestoreData]
public class Admin
{
    [FirestoreProperty]
    public string AdId { get; set; }
    [FirestoreProperty]
    public string AdName { get; set; }
    [FirestoreProperty]
    public string AdFecha { get; set; }
    [FirestoreProperty]
    public bool Ef0 { get; set; }
    [FirestoreProperty]
    public bool Ef1 { get; set; }
    [FirestoreProperty]
    public bool Ef2 { get; set; }
    [FirestoreProperty]
    public bool Ef3 { get; set; }
    [FirestoreProperty]
    public bool Ef4 { get; set; }
    [FirestoreProperty]
    public bool Ef5 { get; set; }
    [FirestoreProperty]
    public bool Ef6 { get; set; }

}
