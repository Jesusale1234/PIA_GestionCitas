using System.ComponentModel.DataAnnotations;

namespace Pia_GestionCitaMedica.Entidades
{
    //Diseño de la tabla Medico
    public class Medico
    {
        [Key] public int Id_Medico { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Especialidad { get; set; }
        public string Email { get; set; }
        public string Contra { get; set; }
    }
}
