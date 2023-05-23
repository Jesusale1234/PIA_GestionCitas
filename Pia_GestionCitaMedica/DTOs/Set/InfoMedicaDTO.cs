using System.ComponentModel.DataAnnotations;

namespace Pia_GestionCitaMedica.DTOs.Set
{
    public class InfoMedicaDTO
    {
        [Required]
        public int Peso { get; set; }
        [Required]
        public int Altura { get; set; }
        public string EnfermedadesAnt { get; set; }
    }
}
