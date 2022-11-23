namespace ApiJugadores.Entidades
{
    public class JugadorEquipo
    {
        public int JugadorId { get; set; }
        public int EquipoId { get; set; }
        public int Orden { get; set; }
        public Jugador Jugador { get; set; }
        public Equipo Equipo { get; set; }
    }
}
