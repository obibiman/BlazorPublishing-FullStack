using System.ComponentModel.DataAnnotations;

namespace Blazor.SankoreAPI.Models.DataTransfer.Login
{
    public class LoginUserDto
    {
        [Required]
        public string Password { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
    }
}
