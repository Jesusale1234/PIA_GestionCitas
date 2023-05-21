using AutoMapper;
using Pia_GestionCitaMedica.DTOs.Get;
using Pia_GestionCitaMedica.DTOs.Set;
using Pia_GestionCitaMedica.Entidades;

namespace Pia_GestionCitaMedica.AutoMapper
{
    public class AutomapperPerfil: Profile
    {
        public AutomapperPerfil()
        {
            //SET
            CreateMap<CitaDTO, Cita>();
            CreateMap<PacienteDTO, Paciente>();


            //GET
            CreateMap<Cita, GetCitaDTO>();
        }
    }
}
