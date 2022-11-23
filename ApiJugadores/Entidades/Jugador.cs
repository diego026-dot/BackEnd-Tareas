using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApiJugadores.Validaciones;

namespace ApiJugadores.Entidades
{
    public class Jugador
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 150, ErrorMessage = "El campo {0} solo puede tener hasta 5 caracteres")]
        [PrimeraLetraMayuscula]
        public string Name { get; set; }

        public List<JugadorEquipo> JugadorEquipo { get; set; }




    }
}
