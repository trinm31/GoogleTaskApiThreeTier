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
            //mapping Task
            config.CreateMap<TaskUpSertDto, TaskItem>();
            config.CreateMap<TaskItem, TaskUpSertDto>();
            //mapping user
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
            // mapping project
            config.CreateMap<ProjectUpSertDto, Project>();
            config.CreateMap<Project, ProjectUpSertDto>();
            // mapping list task
            config.CreateMap<ListTaskUpSertDto, ListTask>();
            config.CreateMap<ListTask, ListTaskUpSertDto>();
            // mapping TodoList
            config.CreateMap<TodoUpSertDto, TodoList>();
            config.CreateMap<TodoList, TodoUpSertDto>();
            // mapping assignment
            config.CreateMap<AssignUpSertDto, Assignment>();
            config.CreateMap<Assignment, AssignUpSertDto>();
        });

        return mappingConfig;
    }
}