using AutoMapper;
using ApiJugadores.DTOs;
using ApiJugadores.Entidades;

namespace ApiJugadores.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<JugadorDTO, Jugador>();
            CreateMap<Jugador, GetJugadorDTO>();
            CreateMap<Jugador, JugadorDTOConEquipos>()
                .ForMember(JugadorDTO => JugadorDTO.Equipos, opciones => opciones.MapFrom(MapJugadorDTOEquipos)); ;
            CreateMap<Equipo, EquipoDTO>();
            CreateMap<Equipo, EquipoDTOConJugadores>()
                .ForMember(equpoDTO => equpoDTO.Jugadores, opciones => opciones.MapFrom(MapEquipoDTOJugadores));

        }

        private List<EquipoDTO> MapJugadorDTOEquipos(Jugador jugador, GetJugadorDTO getJugadorDTO)
        {
            var result = new List<EquipoDTO>();

            if (jugador.JugadorEquipo == null) { return result; }

            foreach (var jugadorEquipo in jugador.JugadorEquipo)
            {
                result.Add(new EquipoDTO()
                {
                    Id = jugadorEquipo.EquipoId,
                    Nombre = jugadorEquipo.Equipo.Nombre
                });
            }

            return result;
        }

        private List<GetJugadorDTO> MapEquipoDTOJugadores(Equipo equipo, EquipoDTO equipoDTO)
        {
            var result = new List<GetJugadorDTO>();

            if (equipo.JugadorEquipo == null)
            {
                return result;
            }

            foreach (var jugadorequipo in equipo.JugadorEquipo)
            {
                result.Add(new GetJugadorDTO()
                {
                    Id = jugadorequipo.JugadorId,
                    Nombre = jugadorequipo.Jugador.Name
                });
            }

            return result;
        }

        private List<JugadorEquipo> MapJugadorEquipo(EquipoCreacionDTO equipoCreacionDTO, Equipo equipo)
        {
            var resultado = new List<JugadorEquipo>();

            if (equipoCreacionDTO.JugadoresIds == null) { return resultado; }
            foreach (var jugadorId in equipoCreacionDTO.JugadoresIds)
            {
                resultado.Add(new JugadorEquipo() { JugadorId = jugadorId });
            }
            return resultado;
        }
    }
}

