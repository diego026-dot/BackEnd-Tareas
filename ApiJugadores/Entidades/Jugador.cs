namespace ApiJugadores.Entidades
{
    public class Jugador
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Equipo> equipos { get; set; }
    }
}
