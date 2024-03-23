namespace IDPBookApp.Models;

public class Tratamiento
{
    public string TId { get; set; }
    public string TFecha { get; set; }
    public int TTipo { get; set; }
    public bool TTipoBool { get; set; }
    public string TPrep { get; set; }
    public int TDosis { get; set; }
    public string TCad { get; set; }
    public bool[] TEfSec { get; set; }
    public string THora { get; set; }
    public int TTimeInf { get; set; }
    public int TVelInf { get; set; }
}
