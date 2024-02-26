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
            cfg.CreateMap<PublisherDTO, Publisher>();
            cfg.CreateMap<BookDTO, Book>();
        });

        var mapper = new Mapper(config);
        return mapper;
    }
}