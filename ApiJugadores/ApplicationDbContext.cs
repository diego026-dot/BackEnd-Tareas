using ApiJugadores.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace ApiJugadores
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options): base(options)
        {
         
        }

        public DbSet<Jugador> jugadores { get; set; }

        public DbSet<Equipo> equipos { get; set; } 
    }
}
