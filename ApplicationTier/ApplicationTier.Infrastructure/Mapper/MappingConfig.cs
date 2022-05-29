using ApplicationTier.Domain.Dtos;
using ApplicationTier.Domain.Entities;
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
        });

        return mappingConfig;
    }
}