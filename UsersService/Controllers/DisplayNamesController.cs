using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using UsersService.Models;
using Shared.Data.Data;

using Shared.Services.Cache;
using Microsoft.EntityFrameworkCore;

namespace UsersService.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}")]
    public class DisplayNamesController : ControllerBase
    {
        private readonly UsersDbContext _context;
        private readonly IRedisCacheService _redisCacheService;

        public DisplayNamesController(UsersDbContext context, IRedisCacheService redisCacheService)
        {
            _context = context;
            _redisCacheService = redisCacheService;
        }
        [HttpGet("display-names/validate")]
        public IActionResult ValidateDisplayName([FromQuery] string displayName, [FromQuery] System.DateTime birthdate)
        {
            // TODO: Implement logic
            return Ok();
        }

        [HttpGet("users/{userId}/display-names/validate")]
        public IActionResult ValidateUserDisplayName([FromRoute] long userId, [FromQuery] string displayName)
        {
            // TODO: Implement logic
            return Ok();
        }

        [HttpPatch("users/{userId}/display-names")]
        public async Task<IActionResult> SetDisplayName([FromRoute] long userId, [FromBody] SetDisplayNameRequest request)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            user.DisplayName = request.NewDisplayName;
            await _context.SaveChangesAsync();

            var cacheKey = $"user:{userId}";
            await _redisCacheService.DeleteAsync(cacheKey);

            return Ok();
        }
    }
}
