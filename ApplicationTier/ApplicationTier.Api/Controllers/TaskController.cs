using ApplicationTier.Domain.Dtos;
using ApplicationTier.Domain.Entities;
using ApplicationTier.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationTier.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
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
        await _taskService.Update(taskItem);
    }

    [HttpGet("{id:int}")]
    public async Task<TaskItem> GetOne([FromRoute] int id)
    {
        return await _taskService.GetOne(id);
    }

    [HttpPost]
    public async Task Add(TaskUpSertDto taskItem)
    {
        await _taskService.Add(taskItem);
    }

    [HttpDelete("{id}")]
    public async Task Delete([FromRoute] int id)
    {
        await _taskService.Delete(id);
    }

    [HttpGet("[action]/{id:int}")]
    public async Task IsDone([FromRoute] int id)
    {
        await _taskService.IsDone(id);
    }

    #endregion
}