using Blazor.SankoreAPI.Models.DataTransfer.Login;
using System.ComponentModel.DataAnnotations;

namespace Blazor.SankoreAPI.Models.DataTransfer.User
{
    public class UserDto : LoginUserDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Role { get; set; }
    }
}