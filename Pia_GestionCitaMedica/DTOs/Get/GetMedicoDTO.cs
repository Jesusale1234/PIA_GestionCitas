using System.ComponentModel.DataAnnotations;

namespace Pia_GestionCitaMedica.DTOs.Get
{
    public class GetMedicoDTO
    {
        [Key] public int Id_Medico { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Especialidad { get; set; }

    }
}
