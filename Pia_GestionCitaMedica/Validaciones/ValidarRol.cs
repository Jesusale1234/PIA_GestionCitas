using System.ComponentModel.DataAnnotations;

namespace Pia_GestionCitaMedica.Validaciones
{
    public class ValidarRol : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }


            if (value.ToString().Equals("medico") || value.ToString().Equals("paciente"))
            {
                return ValidationResult.Success;

            }
            else
            {
                return new ValidationResult("Introduce un rol valido (medico/paciente)");
            }

        }

    }
}
