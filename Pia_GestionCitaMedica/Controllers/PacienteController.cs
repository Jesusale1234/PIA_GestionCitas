using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pia_GestionCitaMedica.DTOs.Get;
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

        /*[HttpPost]
        public ActionResult<List<PacienteDTO>> Get()
        {
            return new List<PacienteDTO>()
            {
                dbContext. PacienteDTO {Id_Paciente=1, Nombre="Jesus",Apellido="Martinez",Edad=19,Telefono="8119336677",Correo="jesus123@gmail.com",Contra="Jesusale123"}
            };
        }*/

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<GetPacienteDTO>>> GetAll()
        {
            logger.LogInformation("Obteniendo Lista de Pacientes...");
            var paciente = await dbContext.Pacientes.ToListAsync();
            return mapper.Map<List<GetPacienteDTO>>(paciente);
        }

         [HttpPost]
         public async Task<ActionResult> Post([FromBody] PacienteDTO pacienteDTO)
         {
             var paciente = mapper.Map<Paciente>(pacienteDTO);
             dbContext.Add(paciente);
             await dbContext.SaveChangesAsync();
             return Ok();
         }


    }
}

