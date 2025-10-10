using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using AuthenticationService.Models;

namespace AuthenticationService.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/revert")]
    public class RevertController : ControllerBase
    {
        [HttpGet("account")]
        public ActionResult<RevertAccountInfoResponse> RevertAccountInfo([FromQuery] string ticket)
        {
            // TODO: Implement logic
            return Ok(new RevertAccountInfoResponse());
        }

        [HttpPost("account")]
        public ActionResult<LoginResponse> RevertAccountSubmit([FromBody] RevertAccountSubmitRequest request)
        {
            // TODO: Implement logic
            return Ok(new LoginResponse());
        }
    }
}
