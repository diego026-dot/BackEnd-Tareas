
namespace ApiJugadores.DTOs
{
    public class JugadorDTOConEquipos : GetJugadorDTO
    {
        public List<EquipoDTO> Equipos { get; set; }
    }
}
