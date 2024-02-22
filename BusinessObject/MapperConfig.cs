using AutoMapper;
using BusinessObject.DTO;
using BusinessObject.Model;

namespace BusinessObject;

public class MapperConfig
{
    public static Mapper Init()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<CategoryDTO, Category>();
        });

        var mapper = new Mapper(config);
        return mapper;
    }
}