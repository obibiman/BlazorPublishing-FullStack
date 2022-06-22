using Microsoft.AspNetCore.Identity;

namespace Blazor.SankoreAPI.Models.Domain
{
    public class ApiUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
