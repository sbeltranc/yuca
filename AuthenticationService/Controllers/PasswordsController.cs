using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using AuthenticationService.Models;

namespace AuthenticationService.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/passwords")]
    public class PasswordsController : ControllerBase
    {
        [HttpPost("validate")]
        public ActionResult<PasswordValidationResponse> ValidateFromBody([FromBody] PasswordValidationModel request)
        {
            // TODO: Implement password validation logic
            return Ok(new PasswordValidationResponse { Code = 0, Message = "ValidPassword" });
        }

        [HttpGet("validate")]
        public ActionResult<PasswordValidationResponse> ValidateFromUri([FromQuery] string username, [FromQuery] string password)
        {
            // TODO: Implement password validation logic
            return Ok(new PasswordValidationResponse { Code = 0, Message = "ValidPassword" });
        }
    }
}
