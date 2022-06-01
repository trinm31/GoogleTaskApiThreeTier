using ApplicationTier.Domain.Dtos;
using ApplicationTier.Domain.Entities;

namespace ApplicationTier.Domain.Interfaces.Services;

public interface IProjectService
{
    Task<IList<Project>> GetAll();
    Task<Project> GetOne(int projectId);
    Task Update(ProjectUpSertDto project, User user);
    Task Add(ProjectUpSertDto project, User user);
    Task Delete(int projectId, User user);
}