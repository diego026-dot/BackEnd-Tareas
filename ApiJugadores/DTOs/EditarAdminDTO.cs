using System.ComponentModel.DataAnnotations;

namespace ApiJugadores.DTOs
{
    public class EditarAdminDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
