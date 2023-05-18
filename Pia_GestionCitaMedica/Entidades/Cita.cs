namespace Pia_GestionCitaMedica.Entidades
{
    //Diseño de la base de datos pala tabla Cita
    public class Cita
    {
        public int Id_Cita { get; set;}
        public int Id_Medico { get; set; }
        public int Id_Paciente { get; set; }
        public int Fecha { get; set; }
        public float Hora { get; set; }
    }
}
