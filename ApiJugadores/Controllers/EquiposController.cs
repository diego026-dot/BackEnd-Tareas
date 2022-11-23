﻿using ApiJugadores.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiJugadores.Controllers
{
    [ApiController]
    [Route("api/equipos")]
    public class EquiposController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public EquiposController(ApplicationDbContext context)
        {
            this.dbContext = context;
        }
   

        [HttpGet]
        public async Task<ActionResult<List<Equipo>>> GetAll()
        {
            return await dbContext.equipos.ToListAsync();
        }

        [HttpGet("{id:int}")]

        public async Task<ActionResult<Equipo>> GetById(int id)
        {
            return await dbContext.equipos.FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]

        public async Task<ActionResult> Post(Equipo equipo)
        {
            var existeJugador = await dbContext.jugadores.AnyAsync(x => x.Id == equipo.Id);

            if (!existeJugador)
            {
                return BadRequest($"No existe el jugador con el id:{equipo.Id}");    
            }
            
            dbContext.Add(equipo);
            await dbContext.SaveChangesAsync();
            return Ok();

        }

        [HttpPut("{id:int}")]

        public async Task<ActionResult> Put(Equipo equipo, int id)
        {
            var exist = await dbContext.jugadores.AnyAsync(x => x.Id == id);

            if (!exist)
            {
                return NotFound("El equipo especifico no existe. ");
            }

            if (equipo.Id != id)
            {
                return BadRequest("El id del equipo no coincide con el establecido en la url. ");
            }

            dbContext.Update(equipo);
            await dbContext.SaveChangesAsync();
            return Ok();

        }

        [HttpDelete("{id:int}")]

        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.equipos.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound("El recurso no fue encontrado. ");
            }

            dbContext.Remove(new Equipo { Id = id, });
            await dbContext.SaveChangesAsync();
            return Ok();
        }


    }
}
