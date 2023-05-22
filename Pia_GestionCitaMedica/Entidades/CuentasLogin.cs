using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Pia_GestionCitaMedica.Entidades
{
    public class CuentasLogin
    {
        [Key] public int Id { get; set; }

        [Required]
        public string IdCuenta { get; set; }
        public IdentityUser Cuenta { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
