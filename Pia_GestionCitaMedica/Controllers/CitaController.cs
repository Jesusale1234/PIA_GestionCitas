using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Pia_GestionCitaMedica.Entidades;
using Pia_GestionCitaMedica.DTOs.Get;
using Pia_GestionCitaMedica.DTOs.Set;

namespace Pia_GestionCitaMedica.Controllers
{
    [ApiController]
    [Route("Cita")]
    public class CitaController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<CitaController> logger;
        private readonly IMapper mapper;
        public CitaController(ApplicationDbContext context, ILogger<CitaController> logger,
            IMapper mapper)
        {
            this.dbContext = context;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CitaDTO citaDTO)
        {
            var PacienteExiste = await dbContext.Citas.AnyAsync(x => x.IdPaciente == citaDTO.Id_Paciente);
            if (!PacienteExiste)
            {
                return BadRequest("El paciente no existe");
            }

            var cita = mapper.Map<CitaDTO>(citaDTO);
            dbContext.Add(cita);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
     }
}