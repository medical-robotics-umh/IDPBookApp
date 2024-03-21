namespace IDPBookApp.Models
{
    public class HistoriaModel
    {
        public string HId { get; set; }
        public string Hfecha { get; set; }
        public string HfDiag { get; set; }
        public bool HActivo { get; set; }
        public bool[] HTDiag { get; set; }
        public int[] HTDiagSub { get; set; }
        public string HTDiagChar { get; set; }
        public string HTDiagSubChar { get; set; }
        public string HAlerg { get; set; }
    }
}
