using System.ComponentModel.DataAnnotations;

namespace Pia_GestionCitaMedica.DTOs.Get
{
    public class GetCitaDTO
    {
        [Key] public int Id_Cita { get; set; }
        public int Id_Medico { get; set; }
        public int Id_Paciente { get; set; }

        [Required]
        public DateOnly Fecha { get; set; }
    }
}
