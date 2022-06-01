using ApplicationTier.Domain.Dtos;
using ApplicationTier.Domain.Entities;

namespace ApplicationTier.Domain.Interfaces.Services;

public interface IListTaskService
{
    Task<IList<ListTask>> GetAll();
    Task<ListTask> GetOne(int listTaskId);
    Task Update(ListTaskUpSertDto listTask, User user);
    Task Add(ListTaskUpSertDto listTask, User user);
    Task Delete(int listTaskId, User user);
}