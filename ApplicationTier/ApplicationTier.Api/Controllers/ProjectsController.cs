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
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProjectsController(IProjectService projectService, IHttpContextAccessor httpContextAccessor)
        {
            _projectService = projectService;
            _httpContextAccessor = httpContextAccessor;
        }

        #region CRUD

        [HttpGet]
        public async Task<IList<Project>> GetAll()
        {
            return await _projectService.GetAll();
        }

        [HttpPut]
        public async Task Update(ProjectUpSertDto projectDto)
        {
            var user = GetCurrentUser();
            await _projectService.Update(projectDto, user);
        }

        [HttpGet("{id:int}")]
        public async Task<Project> GetOne([FromRoute] int id)
        {
            return await _projectService.GetOne(id);
        }

        [HttpPost]
        public async Task Add(ProjectUpSertDto projectDto)
        {
            var user = GetCurrentUser();
            await _projectService.Add(projectDto,user);
        }

        [HttpDelete("{id}")]
        public async Task Delete([FromRoute] int id)
        {
            var user = GetCurrentUser();
            await _projectService.Delete(id, user);
        }

        #endregion

        private User GetCurrentUser()
        {
            return (User)_httpContextAccessor.HttpContext.Items["User"];
        }
    }
}
