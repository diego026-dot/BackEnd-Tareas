using ApiJugadores.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace ApiJugadores.DTOs
{
    public class JugadorDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")] 
        [StringLength(maximumLength: 150, ErrorMessage = "El campo {0} solo puede tener hasta 150 caracteres")]
        [PrimeraLetraMayuscula]
        public string Name { get; set; }
    }
}
