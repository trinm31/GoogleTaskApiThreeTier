using ApplicationTier.Api.Authorization;
using ApplicationTier.Domain.Dtos;
using ApplicationTier.Domain.Entities;
using ApplicationTier.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationTier.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TodosController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITodoListService _todoListService;

        public TodosController(ITodoListService todoListService, IHttpContextAccessor httpContextAccessor)
        {
            _todoListService = todoListService;
            _httpContextAccessor = httpContextAccessor;
        }

        #region CRUD

        [HttpGet]
        public async Task<IList<TodoList>> GetAll()
        {
            return await _todoListService.GetAll();
        }

        [HttpPut]
        public async Task Update(TodoUpSertDto todoUpSertDto)
        {
            var user = GetCurrentUser();
            await _todoListService.Update(todoUpSertDto, user);
        }

        [HttpGet("{id:int}")]
        public async Task<TodoList> GetOne([FromRoute] int id)
        {
            return await _todoListService.GetOne(id);
        }

        [HttpPost]
        public async Task Add(TodoUpSertDto todoUpSertDto)
        {
            var user = GetCurrentUser();
            await _todoListService.Add(todoUpSertDto, user);
        }

        [HttpDelete("{id}")]
        public async Task Delete([FromRoute] int id)
        {
            var user = GetCurrentUser();
            await _todoListService.Delete(id, user);
        }

        [HttpGet("[action]/{id:int}")]
        public async Task IsDone([FromRoute] int id)
        {
            await _todoListService.IsDone(id);
        }

        #endregion

        private User GetCurrentUser()
        {
            return (User)_httpContextAccessor.HttpContext.Items["User"];
        }
    }
}
