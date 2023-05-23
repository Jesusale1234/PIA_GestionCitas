using System.ComponentModel.DataAnnotations;

namespace Pia_GestionCitaMedica.DTOs.Set
{
    public class CitaDTO
    {
        public int Id_Medico { get; set; }
        public int Id_Paciente { get; set; }
        public DateTime Fecha { get; set; }
    }
}
