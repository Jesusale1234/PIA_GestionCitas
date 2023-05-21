using System.ComponentModel.DataAnnotations;

namespace Pia_GestionCitaMedica.Entidades
{
    //Diseño de la tabla Pacientes
    public class Paciente
    {
        [Key] public int Id_Paciente { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Contra { get; set; }
    }
}
