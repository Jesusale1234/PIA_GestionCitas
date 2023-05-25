using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;
using Pia_GestionCitaMedica.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Pia_GestionCitaMedica
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        { 

        }

        public DbSet<Cita> Citas { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<CuentasLogin> CuentasLogins { get; set; }

        public DbSet<InfoMedica> InfoMedicas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Medico>()
                .HasMany(x => x.Citas)
                .WithOne(x => x.Medico)
                .HasForeignKey(x => x.Id_Medico)
                .HasPrincipalKey(x => x.Id_Medico);
                
            modelBuilder.Entity<Paciente>()
                .HasMany(x => x.Citas)
                .WithOne(x => x.Paciente)
                .HasForeignKey(x => x.Id_Paciente)
                .HasPrincipalKey(x => x.Id_Paciente);
        }
    }
}
