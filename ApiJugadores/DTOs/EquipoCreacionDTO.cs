using ApiJugadores.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace ApiJugadores.DTOs
{
    public class EquipoCreacionDTO
    {
        [Required]
        [StringLength(maximumLength: 250, ErrorMessage = "El campo {0} solo puede tener hasta 250 caracteres")]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }

        public DateTime FechaCreacion { get; set; }

        public List<int> JugadoresIds { get; set; }
    }
}
