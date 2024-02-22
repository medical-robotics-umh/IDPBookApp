namespace IDPBookApp.Models
{
    public class EpisodioModel
    {
        public string Enum {  get; set; }
        public bool EAtenPrim { get; set; }
        public bool EUrgHosp{ get; set; }
        public string EDurac { get; set; }
        public bool EIngreso { get; set; }
        public bool EFiebre { get; set; }
        public bool[] ESinCata { get; set; }
    }
}
