using ApplicationTier.Api.Authorization;
using ApplicationTier.Domain.Dtos;
using ApplicationTier.Domain.Entities;
using ApplicationTier.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationTier.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;
    private readonly IHttpContextAccessor _httpContextAccessor;


    public TasksController(ITaskService taskService, IHttpContextAccessor httpContextAccessor)
    {
        _taskService = taskService;
        _httpContextAccessor = httpContextAccessor;
    }

    #region CRUD

    [HttpGet]
    public async Task<IList<TaskItem>> GetAll()
    {
        return await _taskService.GetAll();
    }

    [HttpPut]
    public async Task Update(TaskUpSertDto taskItem)
    {
        var user = GetCurrentUser();
        await _taskService.Update(taskItem, user);
    }

    [HttpGet("{id:int}")]
    public async Task<TaskItem> GetOne([FromRoute] int id)
    {
        return await _taskService.GetOne(id);
    }

    [HttpPost]
    public async Task Add(TaskUpSertDto taskItem)
    {
        var user = GetCurrentUser();
        await _taskService.Add(taskItem, user);
    }

    [HttpDelete("{id}")]
    public async Task Delete([FromRoute] int id)
    {
        var user = GetCurrentUser();
        await _taskService.Delete(id, user);
    }

    [HttpGet("[action]/{id:int}")]
    public async Task IsDone([FromRoute] int id)
    {
        await _taskService.IsDone(id);
    }

    #endregion

    private User GetCurrentUser()
    {
        return (User)_httpContextAccessor.HttpContext.Items["User"];
    }
}