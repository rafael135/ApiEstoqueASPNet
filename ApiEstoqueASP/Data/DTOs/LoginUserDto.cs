using System.ComponentModel.DataAnnotations;

namespace ApiEstoqueASP.Data.DTOs
{
    public class LoginUserDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
