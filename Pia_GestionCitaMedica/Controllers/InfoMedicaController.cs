using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pia_GestionCitaMedica.DTOs.Get;
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

        [HttpGet("InfoMedicaPacientes")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsMedico")]
        public async Task<ActionResult<List<GetInfoMedicaDTO>>> GetAll()
        {
            logger.LogInformation("Obteniendo Información Medica...");
            var medico = await dbContext.InfoMedicas.ToListAsync();
            return mapper.Map<List<GetInfoMedicaDTO>>(medico);
        }

        [HttpGet("InfoMedicaID/{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsPaciente")]
        public async Task<ActionResult<List<GetInfoMedicaDTO>>> ObtenerporId(int id)
        {
            var infomedica = await dbContext.InfoMedicas.Where(x => x.Id_InfoMedica == id).ToListAsync();
            return mapper.Map<List<GetInfoMedicaDTO>>(infomedica);
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsPaciente")]
        public async Task<ActionResult> Modificar(InfoMedicaDTO infoMedicaDTO, [FromRoute] int id)
        {
            var exist = await dbContext.InfoMedicas.AnyAsync(x => x.Id_InfoMedica == id);
            if (!exist)
            {
                return NotFound("No se encontro el paciente");
        
            }
            var infomedica = mapper.Map<InfoMedica>(infoMedicaDTO);
            infomedica.Id_InfoMedica = id;
            dbContext.Update(infomedica);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

    }
}
