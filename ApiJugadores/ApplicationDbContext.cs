using ApiJugadores.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace ApiJugadores
{
    public class ApplicationDbContext: IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options): base(options)
        {
         
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<JugadorEquipo>()
                .HasKey(al => new { al.JugadorId, al.EquipoId });
        }

        public DbSet<Jugador> jugadores { get; set; }

        public DbSet<Equipo> equipos { get; set; } 

        public DbSet<JugadorEquipo> jugadorequipos { get; set; }
    }
}
