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
            cfg.CreateMap<OrderDTO, Order>();
            cfg.CreateMap<OrderDetailDTO, OrderDetail>();
        });

        var mapper = new Mapper(config);
        return mapper;
    }
}