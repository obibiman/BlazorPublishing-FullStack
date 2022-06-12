using AutoMapper;
using Blazor.SankoreAPI.Models.DataTransfer;
using Blazor.SankoreAPI.Models.DataTransfer.Book;
using Blazor.SankoreAPI.Models.Domain;

namespace Blazor.SankoreAPI.Configurations
{
    public class MapperConfig     : Profile
    {
        public MapperConfig()
        {
            CreateMap<AuthorCreateDto, Author>().ReverseMap();
            CreateMap<AuthorUpdateDto, Author>().ReverseMap();
            CreateMap<AuthorReadOnlyDto, Author>().ReverseMap();
            //mapping for Book
            //CreateMap<BookReadOnlyDto, Book>().ReverseMap();
            CreateMap<Book, BookReadOnlyDto>()
                .ForMember(z=>z.AuthorName, y=>y.MapFrom(m=>$"{m.Author.FirstName} {m.Author.LastName}"))
                .ReverseMap();
        }
    }
}
