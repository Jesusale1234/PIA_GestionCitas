using System.ComponentModel.DataAnnotations;

namespace Pia_GestionCitaMedica.DTOs.Get
{
    public class GetPacienteDTO
    {
        [Key] public int Id_Paciente { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }
        public string Telefono { get; set; }

    }
}
