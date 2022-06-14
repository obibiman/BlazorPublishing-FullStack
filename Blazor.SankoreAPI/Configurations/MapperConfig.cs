using AutoMapper;
using Blazor.SankoreAPI.Models.DataTransfer.Author;
using Blazor.SankoreAPI.Models.DataTransfer.Book;
using Blazor.SankoreAPI.Models.Domain;

namespace Blazor.SankoreAPI.Configurations
{
    public class MapperConfig     : Profile
    {
        public MapperConfig()
        {
            //mapping for Author
            CreateMap<AuthorCreateDto, Author>().ReverseMap();
            CreateMap<AuthorUpdateDto, Author>().ReverseMap();
            CreateMap<AuthorReadOnlyDto, Author>().ReverseMap();
            //mapping for Book
            
            //CreateMap<BookReadOnlyDto, Book>().ReverseMap();
            CreateMap<Book, BookReadOnlyDto>()
                .ForMember(z=>z.AuthorName, y=>y.MapFrom(m=>$"{m.Author.FirstName} {m.Author.LastName}"))
                .ReverseMap();
            CreateMap<Book, BookDetailsDto>()
           .ForMember(z => z.AuthorName, y => y.MapFrom(m => $"{m.Author.FirstName} {m.Author.LastName}"))
           .ReverseMap();
            CreateMap<BookUpdateDto, Book>().ReverseMap();
            CreateMap<BookCreateDto, Book>().ReverseMap();
        }
    }
}
