using ApiJugadores.DTOs;
using ApiJugadores.Entidades;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiJugadores.Controllers
{
    [ApiController]
    [Route("api/jugadores")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
    public class JugadoresController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public JugadoresController(ApplicationDbContext context, IMapper mapper, IConfiguration configuration)
        {
            this.dbContext = context;
            this.mapper = mapper;
            this.configuration = configuration;

        }


        [HttpGet]

        public async Task<ActionResult<List<GetJugadorDTO>>> Get()
        {
            var jugadores = await dbContext.jugadores.ToListAsync();
            return mapper.Map<List<GetJugadorDTO>>(jugadores);
        }



        [HttpGet("{id:int}")]

        public async Task<ActionResult<JugadorDTOConEquipos>> Get(int id )
        {
            var jugador = await dbContext.jugadores

                .Include(jugadorDB => jugadorDB.JugadorEquipo)
                .ThenInclude(jugadorEquipo => jugadorEquipo.Equipo)
                .FirstOrDefaultAsync(jugadorDB => jugadorDB.Id == id);

            if (jugador == null)
            {
                return NotFound();
            }

            return mapper.Map<JugadorDTOConEquipos>(jugador);
        }


        [HttpGet("{nombre}")]

        public async Task<ActionResult<List<GetJugadorDTO>>> Get ([FromRoute] string nombre)
        {
            var jugadores = await dbContext.jugadores.Where(jugadorBD => jugadorBD.Name.Contains(nombre)).ToListAsync();

            return mapper.Map<List<GetJugadorDTO>>(jugadores);
        }


        [HttpPost]

        public async Task<ActionResult> Post([FromBody] JugadorDTO jugadorDto)
        {

            var existeJugadorMismoNombre = await dbContext.jugadores.AnyAsync(x => x.Name == jugadorDto.Name);

            if (existeJugadorMismoNombre)
            {
                return BadRequest("Ya existe un autor con el nombre");
            }

            var jugador = mapper.Map<Jugador>(jugadorDto);

            dbContext.Add(jugador);
            await dbContext.SaveChangesAsync();

            var jugadorDTO = mapper.Map<GetJugadorDTO>(jugador);

            return CreatedAtRoute("obtener jugador", new { id = jugador.Id }, jugadorDTO);
        }

    

        [HttpPut("{id:int}")] // api/alumnos/1
        public async Task<ActionResult> Put(JugadorDTO jugadorCreacionDTO, int id)
        {
            var exist = await dbContext.jugadores.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound();
            }

            var jugador = mapper.Map<Jugador>(jugadorCreacionDTO);
            jugador.Id = id;

            dbContext.Update(jugador);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
         public async Task<ActionResult> Delete(int id)
         {
            var exist = await dbContext.jugadores.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound("El Recurso no fue encontrado.");
            }

            dbContext.Remove(new Jugador()
            {
                Id = id
            });
            await dbContext.SaveChangesAsync();
            return Ok();
    }

}
}
