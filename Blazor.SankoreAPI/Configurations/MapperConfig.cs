using AutoMapper;
using Blazor.SankoreAPI.Models.DataTransfer.Author;
using Blazor.SankoreAPI.Models.DataTransfer.Book;
using Blazor.SankoreAPI.Models.DataTransfer.User;
using Blazor.SankoreAPI.Models.Domain;

namespace Blazor.SankoreAPI.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            //mapping for Author
            _ = CreateMap<AuthorCreateDto, Author>().ReverseMap();
            _ = CreateMap<AuthorUpdateDto, Author>().ReverseMap();
            _ = CreateMap<AuthorReadOnlyDto, Author>().ReverseMap();
            //mapping for Book

            //CreateMap<BookReadOnlyDto, Book>().ReverseMap();
            _ = CreateMap<Book, BookReadOnlyDto>()
                .ForMember(z => z.AuthorName, y => y.MapFrom(m => $"{m.Author.FirstName} {m.Author.LastName}"))
                .ReverseMap();
            _ = CreateMap<Book, BookDetailsDto>()
           .ForMember(z => z.AuthorName, y => y.MapFrom(m => $"{m.Author.FirstName} {m.Author.LastName}"))
           .ReverseMap();
            _ = CreateMap<BookUpdateDto, Book>().ReverseMap();
            _ = CreateMap<BookCreateDto, Book>().ReverseMap();

            //mapping for User
            _ = CreateMap<ApiUser, UserDto>().ReverseMap();
        }
    }
}
