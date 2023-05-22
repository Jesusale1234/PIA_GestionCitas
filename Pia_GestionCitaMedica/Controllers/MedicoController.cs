using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Pia_GestionCitaMedica.DTOs;
using Pia_GestionCitaMedica.DTOs.Get;
using Pia_GestionCitaMedica.DTOs.Set;
using Pia_GestionCitaMedica.Entidades;

namespace Pia_GestionCitaMedica.Controllers
{
    [ApiController]
    [Route("Medico")]
    public class MedicoController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<CitaController> logger;
        private readonly IMapper mapper;

        public MedicoController(ApplicationDbContext context, ILogger<CitaController> logger,
            IMapper mapper)
        {
            this.dbContext = context;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpPost("Registrar")]
        public async Task<ActionResult> Post([FromBody] MedicoDTO medicoDTO)
        {
            var medico = mapper.Map<Medico>(medicoDTO);
            dbContext.Add(medico);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("Todos")]
        public async Task<ActionResult<List<GetMedicoDTO>>> GetAll()
        {
            logger.LogInformation("Obteniendo Lista de Medicos...");
            var medico = await dbContext.Medicos.ToListAsync();
            return mapper.Map<List<GetMedicoDTO>>(medico);
        }



    }
}

    /*[HttpGet]

            public ActionResult<List<Medico>> Get()
            {
                return new List<Medico>()
                    {

                };
            }*/

