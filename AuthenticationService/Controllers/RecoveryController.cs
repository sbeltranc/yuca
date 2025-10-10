using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using AuthenticationService.Models;

namespace AuthenticationService.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/recovery")]
    public class RecoveryController : ControllerBase
    {
        [HttpGet("metadata")]
        public ActionResult<RecoveryMetadataResponse> GetMetadata()
        {
            // TODO: Implement logic
            return Ok(new RecoveryMetadataResponse());
        }
    }
}
