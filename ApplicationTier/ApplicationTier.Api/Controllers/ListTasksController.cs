using ApplicationTier.Api.Authorization;
using ApplicationTier.Domain.Dtos;
using ApplicationTier.Domain.Entities;
using ApplicationTier.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationTier.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ListTasksController : ControllerBase
    {
        private readonly IListTaskService _listTaskService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public ListTasksController(IListTaskService listTaskService, IHttpContextAccessor httpContextAccessor)
        {
            _listTaskService = listTaskService;
            _httpContextAccessor = httpContextAccessor;
        }

        #region CRUD
        [HttpGet]
        public async Task<IList<ListTask>> GetAll()
        {
            return await _listTaskService.GetAll();
        }

        [HttpPut]
        public async Task Update(ListTaskUpSertDto listTaskDto)
        {
            var user = GetCurrentUser();
            await _listTaskService.Update(listTaskDto, user);
        }

        [HttpGet("{id:int}")]
        public async Task<ListTask> GetOne([FromRoute] int id)
        {
            return await _listTaskService.GetOne(id);
        }

        [HttpPost]
        public async Task Add(ListTaskUpSertDto listTaskDto)
        {
            var user = GetCurrentUser();
            await _listTaskService.Add(listTaskDto, user);
        }

        [HttpDelete("{id}")]
        public async Task Delete([FromRoute] int id)
        {
            var user = GetCurrentUser();
            await _listTaskService.Delete(id, user);
        }

        #endregion

        private User GetCurrentUser()
        {
            return (User)_httpContextAccessor.HttpContext.Items["User"];
        }
    }
}
