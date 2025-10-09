using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using AuthenticationService.Data;
using AuthenticationService.Models;
using System.Threading.Tasks;

namespace AuthenticationService.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthDbContext _context;

        public AuthenticationController(AuthDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Authentication service v1 is running.");
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
        {
            // TODO: Implement login logic using Argon2 to verify password hash
            // For now, returning a placeholder response
            return Ok(new LoginResponse());
        }
    }
}
