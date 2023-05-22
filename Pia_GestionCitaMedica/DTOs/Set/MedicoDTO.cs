using System.ComponentModel.DataAnnotations;

namespace Pia_GestionCitaMedica.DTOs.Set
{
    public class MedicoDTO
    {
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Especialidad { get; set; }
        public string Email { get; set; }
        public string Contra { get; set; }
    }
}
