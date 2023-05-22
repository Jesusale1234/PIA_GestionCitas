using System.ComponentModel.DataAnnotations;

namespace Pia_GestionCitaMedica.DTOs.Set
{
    public class PacienteDTO
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }
        public string Telefono { get; set; }

    }
}
