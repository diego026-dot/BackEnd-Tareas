using ApiJugadores.Entidades;
using ApiJugadores.Filtros;
using ApiJugadores.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiJugadores.Controllers
{
    [ApiController]
    [Route("api/jugadores")]
    public class JugadoresController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IService service;
        private readonly ServiceTransient serviceTransient;
        private readonly ServiceScoped serviceScoped;
        private readonly ServiceSingleton serviceSingleton;
        private readonly ILogger<JugadoresController> logger;

        public JugadoresController(ApplicationDbContext context, IService service, ServiceTransient serviceTransient, ServiceScoped serviceScoped,
            ServiceSingleton serviceSingleton, ILogger<JugadoresController> logger)
        {
            this.dbContext = context;
            this.service = service;
            this.serviceTransient = serviceTransient;
            this.serviceScoped = serviceScoped;
            this.serviceSingleton = serviceSingleton;
            this.logger = logger;
        }

        [HttpGet("GUID")]
        [ResponseCache(Duration = 10)]
        [ServiceFilter(typeof(FiltroDeAccion))]

        public ActionResult ObtenerGuid()
        {
            throw new NotImplementedException();
            logger.LogInformation("Durante la ejecucion");
            return Ok(new
            {
                AlumnosControllerTransient = serviceTransient.guid,
                ServiceA_Transient = service.GetTransient(),
                AlumnosControllerScoped = serviceScoped.guid,
                ServiceA_Scoped = service.GetScoped(),
                AlumnosControllerSingleton = serviceSingleton.guid,
                ServiceA_Singleton = service.GetSingleton()
            });
        }


        [HttpGet("Listado")]
        [HttpGet("/listado")]
        public async Task<ActionResult<List<Jugador>>> Get()
        {
            throw new NotImplementedException();
            logger.LogInformation("Se obtiene el listado de alumnos");
            logger.LogWarning("Mensaje de prueba warning");
            service.EjecutarJob();
            return await dbContext.jugadores.Include(x => x.equipos).ToListAsync();
        }

        [HttpGet("Primero")]

        public async Task<ActionResult<Jugador>> PrimerJugador()
        {
            return await dbContext.jugadores.FirstOrDefaultAsync(); 
        }

        [HttpGet("primero2")] 
        public ActionResult<Jugador> PrimerAlumnoD()
        {
            return new Jugador { Name = "Messi" };
        }


        [HttpGet("{id:int}/{param?}")]

        public async Task<ActionResult<Jugador>> Get(int id, string param )
        {
            var jugador = await dbContext.jugadores.FirstOrDefaultAsync(x => x.Id == id);

            if (jugador == null)
            {
                return NotFound();
            }

            return jugador;
        }


        [HttpGet("{nombre}")]

        public async Task<ActionResult<Jugador>> Get ([FromRoute] string nombre)
        {
            var jugador = await dbContext.jugadores.FirstOrDefaultAsync(x => x.Name.Contains(nombre));

            if(jugador == null)
            {
                logger.LogError("No se encuentra el jugador");
                return NotFound();
            }

            return jugador;
        }


        [HttpPost]

        public async Task<ActionResult> Post([FromBody] Jugador jugador)
        {

            var existeJugadorMismoNombre = await dbContext.jugadores.AnyAsync(x => x.Name == jugador.Name);

            if (existeJugadorMismoNombre)
            {
                return BadRequest("Ya existe un autor con el nombre");
            }


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
