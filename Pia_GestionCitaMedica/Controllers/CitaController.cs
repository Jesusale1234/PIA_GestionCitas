using Microsoft.AspNetCore.Mvc;
using Pia_GestionCitaMedica.Entidades;

namespace Pia_GestionCitaMedica.Controllers
{
    [ApiController]
    [Route("Cita")]
    public class CitaController : ControllerBase
    {
        [HttpGet]

        public ActionResult<List<Cita>>Get()
        {
            return new List<Cita>()
            {

            };
        }

    }
}
