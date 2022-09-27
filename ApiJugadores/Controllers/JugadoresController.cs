using ApiJugadores.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiJugadores.Controllers
{
    [ApiController]
    [Route("jugadores")]
    public class JugadoresController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public JugadoresController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Jugador>>> Get()
        {
            return await dbContext.jugadores.Include(x => x.equipos).ToListAsync();
        }

        [HttpPost]

        public async Task<ActionResult> Post(Jugador jugador)
        {
            dbContext.Add(jugador);
            await dbContext.SaveChangesAsync();
            return Ok();

        }

        [HttpPut("{id:int}")]

        public async Task<ActionResult> Put(Jugador jugador, int id)
        {
            if (jugador.Id != id)
            {
                return BadRequest("El id del jugador no coincide con el establecido en la url");
            }

            dbContext.Update(jugador);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]

        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.jugadores.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound();
            }

            dbContext.Remove(new Jugador()
            {
                Id = id,
            });
            await dbContext.SaveChangesAsync();
            return Ok();
        }

    }
}
