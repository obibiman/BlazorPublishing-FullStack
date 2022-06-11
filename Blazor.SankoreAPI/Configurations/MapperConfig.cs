using AutoMapper;
using Blazor.SankoreAPI.Models.DataTransfer;
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
        }
    }
}
