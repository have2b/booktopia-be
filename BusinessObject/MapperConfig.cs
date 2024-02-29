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
            cfg.CreateMap<UserDTO, User>();
            cfg.CreateMap<User, UserDTO>();
            cfg.CreateMap<UserUpdateInfomationRequest, User>();
        });

        var mapper = new Mapper(config);
        return mapper;
    }
}