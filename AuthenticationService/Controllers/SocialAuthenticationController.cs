using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using AuthenticationService.Models;

namespace AuthenticationService.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/social")]
    public class SocialAuthenticationController : ControllerBase
    {
        [HttpGet("connected-providers")]
        public ActionResult<SocialProvidersResponse> GetConnectedProviders()
        {
            // TODO: Implement logic
            return Ok(new SocialProvidersResponse());
        }

        [HttpPost("{provider}/disconnect")]
        public ActionResult Disconnect([FromRoute] string provider, [FromBody] SocialAuthenticationDisconnectRequest request)
        {
            // TODO: Implement logic
            return Ok();
        }
    }
}
