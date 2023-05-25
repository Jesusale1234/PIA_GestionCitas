using AutoMapper;
using Pia_GestionCitaMedica.DTOs;
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
            CreateMap<MedicoDTO, Medico>();
            CreateMap<CredencialesUsuario, CuentasLogin>();
            CreateMap<InfoMedicaDTO, InfoMedica>();


            //GET
            CreateMap<Cita, GetCitaDTO>();
            CreateMap<Paciente, GetPacienteDTO>();
            CreateMap<Medico, GetMedicoDTO>();
            CreateMap<InfoMedica, GetInfoMedicaDTO>();
        }
    }
}
