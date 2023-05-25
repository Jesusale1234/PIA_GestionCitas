using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Pia_GestionCitaMedica.Entidades;
using Pia_GestionCitaMedica.DTOs.Get;
using Pia_GestionCitaMedica.DTOs.Set;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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

        [HttpGet("ResumenCitas")]

        public async Task<ActionResult<List<GetCitaDTO>>> GetAll()
        {
            logger.LogInformation("Obteniendo Citas...");
            var citas = await dbContext.Citas.ToListAsync();
            return mapper.Map<List<GetCitaDTO>>(citas);
        }

        [HttpGet("ConsultaPaciente/{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsPaciente")]
        public async Task<ActionResult<List<GetCitaDTO>>> ConsultarCita(int id)
        {
            var cita = await dbContext.Citas.Where(x => x.Id_Paciente == id).ToListAsync();
            return mapper.Map<List<GetCitaDTO>>(cita);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsMedico")]
        public async Task<ActionResult> Post([FromBody] CitaDTO citaDTO)
        {
            var MedicoExiste = await dbContext.Medicos.AnyAsync(x => x.Id_Medico == citaDTO.Id_Medico);
            if (!MedicoExiste)
            {
                return BadRequest("El medico no existe");
            }

            var PacienteExiste = await dbContext.Pacientes.AnyAsync(x => x.Id_Paciente == citaDTO.Id_Paciente);
            if (!PacienteExiste)
            {
                return BadRequest("El paciente no existe");
            }

            var Horario = await dbContext.Citas.AnyAsync(x => x.Fecha == citaDTO.Fecha);

            if(MedicoExiste && Horario)
            {
                return BadRequest("El horario esta ocupado");
            }


            var cita = mapper.Map<Cita>(citaDTO);
            dbContext.Add(cita);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsMedico")]
        public async Task<ActionResult> PutCita(CitaDTO citaDTO, [FromRoute] int id)
        {
     
            var CitaExiste = await dbContext.Citas.AnyAsync(x => x.Id_Cita == id);
            if (!CitaExiste)
            {
                return BadRequest("No existe la cita");
            }

            var MedicoExist = await dbContext.Medicos.AnyAsync(x => x.Id_Medico == citaDTO.Id_Medico);
            if(!MedicoExist)
            {
                return BadRequest("No existe medico");
            }

            var Horario = await dbContext.Citas.AnyAsync(x => x.Fecha == citaDTO.Fecha);

            if (MedicoExist && Horario)
            {
                return BadRequest("El horario esta ocupado");
            }

            var cita = mapper.Map<Cita>(citaDTO);
            cita.Id_Cita = id;
            cita.Fecha = citaDTO.Fecha;
            dbContext.Update(cita);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsMedico")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Citas.AnyAsync(x => x.Id_Cita == id);
            if (!exist)
            {
                return BadRequest("La cita no existe");
            }
            dbContext.Remove(new Cita()
            {
                Id_Cita = id
            });
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("Id_Medico/{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsMedico")]
        public async Task<ActionResult<List<GetCitaDTO>>> CitaporIdMed(int id)
        {
            var cita = await dbContext.Citas.Where(x => x.Id_Medico == id).ToListAsync();
            return mapper.Map<List<GetCitaDTO>>(cita);
        }

        [HttpGet("Fecha")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsMedico")]
        public async Task<ActionResult<List<GetCitaDTO>>> CitaporFecha(DateTime Fecha)
        {
            var Inicio = new DateTime(Fecha.Year, Fecha.Month, Fecha.Day, 0, 0, 1);
            var Fin = new DateTime(Fecha.Year, Fecha.Month, Fecha.Day, 23, 59, 59);
            var cita = await dbContext.Citas.Where(x => x.Fecha > Inicio && x.Fecha < Fin).ToListAsync();
            return mapper.Map<List<GetCitaDTO>>(cita);
        }
     }
}