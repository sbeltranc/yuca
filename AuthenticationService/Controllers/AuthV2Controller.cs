using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/auth")]
    public class AuthV2Controller : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Authentication service v2 is running.");
        }
    }
}
