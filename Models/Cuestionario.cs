using Google.Cloud.Firestore;

namespace IDPBookApp.Models;

[FirestoreData]
public class Cuestionario
{
    [FirestoreProperty]
    public string QId { get; set; }
    [FirestoreProperty]
    public string QName { get; set; }
    [FirestoreProperty]
    public string QFecha { get; set; }
    [FirestoreProperty]
    public int QMovil { get; set; }
    [FirestoreProperty]
    public int QCuid { get; set; }
    [FirestoreProperty]
    public int QActDia { get; set; }
    [FirestoreProperty]
    public int QDolor { get; set; }
    [FirestoreProperty]
    public int QAnsd { get; set; }
    [FirestoreProperty]
    public double QEscala { get; set; }
    [FirestoreProperty]
    public string QDesc { get; set; }
}
