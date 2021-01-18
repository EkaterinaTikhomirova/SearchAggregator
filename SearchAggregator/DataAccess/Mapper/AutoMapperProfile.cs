using AutoMapper;
using SearchAggregator.DataAccess.DTOs;
using SearchAggregator.DataAccess.Models;

namespace SearchAggregator.DataAccess.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ResourceDTO, Resource>().ReverseMap();
            CreateMap<KeywordDTO, Keyword>();
        }
    }
}
