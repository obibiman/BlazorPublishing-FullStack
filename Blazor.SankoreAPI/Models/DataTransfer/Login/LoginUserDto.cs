using System.ComponentModel.DataAnnotations;

namespace Blazor.SankoreAPI.Models.DataTransfer.Login
{
    public class LoginUserDto
    {      
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
