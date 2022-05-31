using ApplicationTier.Domain.Dtos;
using ApplicationTier.Domain.Entities;
using ApplicationTier.Domain.Models.Users;
using AutoMapper;

namespace ApplicationTier.Infrastructure.Mapper;

public class MappingConfig 
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<TaskUpSertDto, TaskItem>();
            config.CreateMap<TaskItem, TaskUpSertDto>();
            config.CreateMap<User, AuthenticateResponse>();
            config.CreateMap<RegisterRequest, User>();
            config.CreateMap<UpdateRequest, User>()
                .ForAllMembers(x => x.Condition(
                    (src, dest, prop) =>
                    {
                        // ignore null & empty string properties
                        if (prop == null) return false;
                        if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                        return true;
                    }
                ));

        });

        return mappingConfig;
    }
}