using System.ComponentModel.DataAnnotations;

namespace Pia_GestionCitaMedica.Entidades
{
    public class InfoMedica
    {
        [Key] public int Id_InfoMedica { get; set; }
        [Required]
        public int Peso { get; set; }
        [Required]
        public int Altura { get; set; }
        public string EnfermedadesAnt { get;  set; }
    }
}
