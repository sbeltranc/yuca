using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using UsersService.Models;
using UsersService.Data;

namespace UsersService.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/users/{userId}")]
    public class UsernamesController : ControllerBase
    {
        private readonly UsersDbContext _context;

        public UsernamesController(UsersDbContext context)
        {
            _context = context;
        }
        [HttpGet("username-history")]
        public ActionResult<ApiPageResponse<UsernameHistoryResponse>> GetUsernameHistory(
            [FromRoute] long userId,
            [FromQuery] int limit = 10,
            [FromQuery] string cursor = null,
            [FromQuery] string sortOrder = "Asc")
        {
            // TODO: Implement logic
            return Ok(new ApiPageResponse<UsernameHistoryResponse>());
        }
    }
}
