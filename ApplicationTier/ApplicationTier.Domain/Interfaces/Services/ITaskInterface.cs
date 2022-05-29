using ApplicationTier.Domain.Dtos;
using ApplicationTier.Domain.Entities;

namespace ApplicationTier.Domain.Interfaces.Services;

public interface ITaskService
{
    /// <summary>
    /// Get all items of Task table
    /// </summary>
    /// <returns></returns>
    Task<IList<TaskItem>> GetAll();
    Task<TaskItem> GetOne(int taskId);
    Task Update(TaskUpSertDto taskItemDto);
    Task Add(TaskUpSertDto taskItemDto);
    Task Delete(int taskId);
    Task IsDone(int taskId);
}