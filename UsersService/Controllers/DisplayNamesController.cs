using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using UsersService.Models;
using Shared.Data.Data;

namespace UsersService.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}")]
    public class DisplayNamesController : ControllerBase
    {
        private readonly UsersDbContext _context;

        public DisplayNamesController(UsersDbContext context)
        {
            _context = context;
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
        public IActionResult SetDisplayName([FromRoute] long userId, [FromBody] SetDisplayNameRequest request)
        {
            // TODO: Implement logic
            return Ok();
        }
    }
}
