using Blazor.SankoreAPI.Models.DataTransfer.Author;
using Microsoft.AspNetCore.Mvc;

namespace Blazor.SankoreAPI.Models.Domain
{
    public partial class Author
    {
        public Author()
        {
            Books = new HashSet<Book>();
        }

        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Bio { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
