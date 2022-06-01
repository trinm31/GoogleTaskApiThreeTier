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
    public class AssignmentsController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAssignService _assignService;

        public AssignmentsController(IAssignService assignService, IHttpContextAccessor httpContextAccessor)
        {
            _assignService = assignService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        public async Task Add(AssignUpSertDto assignUpSertDto)
        {
            var user = GetCurrentUser();
            await _assignService.Add(assignUpSertDto, user);
        }

        [HttpDelete("{id}")]
        public async Task Delete([FromRoute] int id)
        {
            var user = GetCurrentUser();
            await _assignService.Delete(id, user);
        }

        private User GetCurrentUser()
        {
            return (User)_httpContextAccessor.HttpContext.Items["User"];
        }
    }
}
