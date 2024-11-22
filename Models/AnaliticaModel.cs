using Google.Cloud.Firestore;

namespace IDPBookApp.Models;

[FirestoreData]
public class AnaliticaModel
{
    [FirestoreProperty]
    public string AId { get; set; }
    [FirestoreProperty]
    public string AName { get; set; }
    [FirestoreProperty]
    public string AFecha { get; set; }
    [FirestoreProperty]
    public int AIgG { get; set; }
    [FirestoreProperty]
    public int AIgA { get; set; }
    [FirestoreProperty]
    public int AIgM { get; set; }
    [FirestoreProperty]
    public int AIgG1 { get; set; }
    [FirestoreProperty]
    public int AIgG2 { get; set; }
    [FirestoreProperty]
    public int AIgG3 { get; set; }
    [FirestoreProperty]
    public int AIgG4 { get; set; }
    [FirestoreProperty]
    public int AHbA1c { get; set; }
    [FirestoreProperty]
    public int AHDL { get; set; }
    [FirestoreProperty]
    public int ALDL { get; set; }
    [FirestoreProperty]
    public int ATG { get; set; }
    [FirestoreProperty]
    public int AColesT { get; set;}
    [FirestoreProperty]
    public int AHemo { get; set; }
    [FirestoreProperty]
    public int ALinfo { get; set; }
    [FirestoreProperty]
    public int ANeuro { get; set; }
    [FirestoreProperty]
    public int APlaque { get; set; }
}
