using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using AuthenticationService.Models;

using Shared.Services.Cache;
using Shared.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/revert")]
    public class RevertController : ControllerBase
    {
        private readonly IRedisCacheService _redisCacheService;
        private readonly UsersDbContext _usersDbContext;

        public RevertController(IRedisCacheService redisCacheService, UsersDbContext usersDbContext)
        {
            _redisCacheService = redisCacheService;
            _usersDbContext = usersDbContext;
        }

        [HttpGet("account")]
        public ActionResult<RevertAccountInfoResponse> RevertAccountInfo([FromQuery] string ticket)
        {
            // TODO: Implement logic
            return Ok(new RevertAccountInfoResponse());
        }

        [HttpPost("account")]
        public async Task<ActionResult<LoginResponse>> RevertAccountSubmit([FromBody] RevertAccountSubmitRequest request)
        {
            // TODO: Implement logic to get user from ticket
            // For now, I'll assume we have the user id
            long userId = 1; // placeholder

            var user = await _usersDbContext.Users.FindAsync(userId);
            if (user != null)
            {
                var cacheKey = $"user:{user.Id}";
                await _redisCacheService.DeleteAsync(cacheKey);

                var usernameCacheKey = $"user:Username:{user.Name}";
                await _redisCacheService.DeleteAsync(usernameCacheKey);
            }

            return Ok(new LoginResponse());
        }
    }
}
