namespace IDPBookApp.Models;

public class Tratamiento
{
    public string TFecha { get; set; }
    public int TTipo { get; set; }
    public int TPrep { get; set; }
    public int TDosis { get; set; }
    public int TCad { get; set; }
    public bool[] TEfSec { get; set; }
    public string THora { get; set; }
    public int TTimeInf { get; set; }
    public int TVelInf { get; set; }
}
