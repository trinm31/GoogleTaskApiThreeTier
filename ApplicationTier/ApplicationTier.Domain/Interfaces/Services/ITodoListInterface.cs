using ApplicationTier.Domain.Dtos;
using ApplicationTier.Domain.Entities;

namespace ApplicationTier.Domain.Interfaces.Services;

public interface ITodoListService
{
    Task<IList<TodoList>> GetAll();
    Task<TodoList> GetOne(int todoId);
    Task Update(TodoUpSertDto todoDto, User user);
    Task Add(TodoUpSertDto todoDto, User user);
    Task Delete(int todoId, User user);
    Task IsDone(int todoId);
}