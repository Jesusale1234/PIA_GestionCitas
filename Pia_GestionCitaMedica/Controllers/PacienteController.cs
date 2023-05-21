using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pia_GestionCitaMedica.DTOs.Set;
using Pia_GestionCitaMedica.Entidades;

namespace Pia_GestionCitaMedica.Controllers
{
    [ApiController]
    [Route("Paciente")]
    public class PacienteController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<CitaController> logger;
        private readonly IMapper mapper;
        public PacienteController(ApplicationDbContext context, ILogger<CitaController> logger,
            IMapper mapper)
        {
            this.dbContext = context;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PacienteDTO pacienteDTO)
        {
            var paciente = mapper.Map<PacienteDTO>(pacienteDTO);
            dbContext.Add(paciente);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
