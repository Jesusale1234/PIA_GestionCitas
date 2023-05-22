﻿using System.ComponentModel.DataAnnotations;

namespace Pia_GestionCitaMedica.Entidades
{
    //Diseño de la base de datos pala tabla Cita
    public class Cita
    {
        [Key] public int Id_Cita { get; set; }
        public int IdMedico { get; set; }
        public Medico Medico { get; set; }
        public int IdPaciente { get; set; }
        public Paciente Paciente { get; set; }
        public DateTime Fecha { get; set; } 
    }
}
