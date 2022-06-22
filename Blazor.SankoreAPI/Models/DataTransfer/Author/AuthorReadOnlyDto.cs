using System.ComponentModel.DataAnnotations;

namespace Blazor.SankoreAPI.Models.DataTransfer.Author
{
    public class AuthorReadOnlyDto : BaseDto
    {
        [Required, StringLength(50)]
        public string FirstName { get; set; }
        [Required, StringLength(50)]

        public string LastName { get; set; }
        [StringLength(250)]

        public string Bio { get; set; } = string.Empty;
    }
}
