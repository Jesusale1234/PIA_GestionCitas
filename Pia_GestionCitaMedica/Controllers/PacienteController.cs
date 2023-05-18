using Microsoft.AspNetCore.Mvc;
using Pia_GestionCitaMedica.Entidades;

namespace Pia_GestionCitaMedica.Controllers
{
    [ApiController]
    [Route("Paciente")]
    public class PacienteController : ControllerBase
    {
        [HttpGet]

        public ActionResult<List<Paciente>> Get()
        {
            return new List<Paciente>()
            {

            };
        }
    }
}
