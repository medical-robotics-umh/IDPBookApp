using Google.Cloud.Firestore;

namespace IDPBookApp.Models;

[FirestoreData]
public class Paciente
{
    [FirestoreProperty]
    public string IdMed { get; set; }
    [FirestoreProperty]
    public int Cntr { get; set; }
    [FirestoreProperty]
    public string Nombre { get; set; }
    [FirestoreProperty]
    public string Apelld { get; set; }
    [FirestoreProperty]
    public string Correo { get; set; }
    [FirestoreProperty]
    public string Gener { get; set; }
    [FirestoreProperty]
    public string FechNac {  get; set; }
    [FirestoreProperty]
    public int Diagnsc { get; set; }
    [FirestoreProperty]
    public string FechDiag { get; set; }
    [FirestoreProperty]
    public string OtroDiag1 { get; set; }
    [FirestoreProperty]
    public string FechDiag1 { get; set; }
    [FirestoreProperty]
    public string OtroDiag2 { get; set; }
    [FirestoreProperty]
    public string FechDiag2 { get; set; }
    [FirestoreProperty]
    public string Pass { get; set; }
}
