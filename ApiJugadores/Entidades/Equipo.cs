﻿namespace ApiJugadores.Entidades
{
    public class Equipo
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Pais { get; set; }

        public List<JugadorEquipo> JugadorEquipo { get; set; }


    }
}
