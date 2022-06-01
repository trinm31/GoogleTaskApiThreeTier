using ApplicationTier.Domain.Dtos;
using ApplicationTier.Domain.Entities;

namespace ApplicationTier.Domain.Interfaces.Services;

public interface IAssignService
{
    Task Add(AssignUpSertDto assignDto, User user);
    Task Delete(int assignmentId, User user);
}