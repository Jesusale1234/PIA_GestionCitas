using Microsoft.AspNetCore.Mvc;
using Pia_GestionCitaMedica.Entidades;

namespace Pia_GestionCitaMedica.Controllers
{
    [ApiController]
    [Route("Medico")]
    public class MedicoController : ControllerBase
    {
        [HttpGet]

        public ActionResult<List<Medico>> Get()
        {
            return new List<Medico>()
                {

            };
        }
        


    }
}
