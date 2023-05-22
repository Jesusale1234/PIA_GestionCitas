using System.ComponentModel.DataAnnotations;

namespace Pia_GestionCitaMedica.DTOs
{
    public class CredencialesUsuario
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Pass { get; set; }

        [Required]
        public string Rol { get; set; }
    }
}
