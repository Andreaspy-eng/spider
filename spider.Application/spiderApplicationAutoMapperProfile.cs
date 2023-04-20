using AutoMapper;
using spider.Products;
using spider.Yandex;

namespace spider;

public class spiderApplicationAutoMapperProfile : Profile
{
    public spiderApplicationAutoMapperProfile()
    {
        CreateMap<ResultToken, ResultTokenDTO>();
        CreateMap<CrUpResultToken, ResultToken>();
    }
}
