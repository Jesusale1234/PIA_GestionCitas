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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsMedico")]
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

            var cita = mapper.Map<Cita>(citaDTO);
            dbContext.Add(cita);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutCita(CitaDTO citaDTO, [FromRoute] int id)
        {
     
            var exist = await dbContext.Citas.AnyAsync(x => x.Id_Cita == id);
            if (!exist)
            {
                return BadRequest("No existe la cita");
            }

            var MedicoExist = await dbContext.Medicos.AnyAsync(x => x.Id_Medico == citaDTO.Id_Medico);
            if(!MedicoExist)
            {
                return BadRequest("No existe medico");
            }

            var cita = mapper.Map<Cita>(citaDTO);
            cita.Id_Cita = id;
            cita.Fecha = DateTime.Now;
            dbContext.Update(cita);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
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

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetCitaDTO>> ConsultarCita(int id)
        {
            var cita = await dbContext.Pacientes.AnyAsync(x => x.Id_Paciente == id);
            return mapper.Map<GetCitaDTO>(cita);
        }
     }
}