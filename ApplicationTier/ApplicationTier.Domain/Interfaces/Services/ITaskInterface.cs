using ApplicationTier.Domain.Dtos;
using ApplicationTier.Domain.Entities;

namespace ApplicationTier.Domain.Interfaces.Services;

public interface ITaskService
{
    Task<IList<TaskItem>> GetAll();
    Task<TaskItem> GetOne(int taskId);
    Task Update(TaskUpSertDto taskItemDto, User user);
    Task Add(TaskUpSertDto taskItemDto, User user);
    Task Delete(int taskId, User user);
    Task IsDone(int taskId);
}