using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pia_GestionCitaMedica.DTOs.Set;
using Pia_GestionCitaMedica.Entidades;

namespace Pia_GestionCitaMedica.Controllers
{
    [ApiController]
    [Route("InfoMedica")]
    public class InfoMedicaController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<CitaController> logger;
        private readonly IMapper mapper;

        public InfoMedicaController(ApplicationDbContext context, ILogger<CitaController> logger,
            IMapper mapper)
        {
            this.dbContext = context;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpPost("Registrar")]
        public async Task<ActionResult> Post([FromBody] InfoMedicaDTO infoMedicaDTO)
        {
            var infomedica = mapper.Map<InfoMedica>(infoMedicaDTO);
            dbContext.Add(infomedica);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("Modificar")]
        public async Task<ActionResult> Modificar(InfoMedicaDTO infoMedicaDTO, [FromRoute] int id)
        {
            var exist = await dbContext.Pacientes.AnyAsync(x => x.Id_Paciente == id);
            if (!exist)
            {
                return NotFound("No se encontro el paciente");
        
            }
            var infomedica = mapper.Map<InfoMedica>(infoMedicaDTO);
            dbContext.Update(infomedica);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

    }
}
