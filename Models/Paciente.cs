namespace IDPBookApp.Models
{
    public class Paciente
    {
        public string IdMed { get; set; }
        public string Nombre { get; set; }
        public string Apelld { get; set; }
        public string Correo { get; set; }
        public int Sexo { get; set; }
        public string FechNac {  get; set; }
        public string Edad { get; set; }
        public bool TratAct { get; set; }
        public bool[] Diagnsc { get; set; }
        public string OtroDiag { get; set; }
        public string FechDiag { get; set; }
    }
}
