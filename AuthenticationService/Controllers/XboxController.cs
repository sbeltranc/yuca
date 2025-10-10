using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using AuthenticationService.Models;

namespace AuthenticationService.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/xbox")]
    public class XboxController : ControllerBase
    {
        [HttpGet("connection")]
        public ActionResult<XboxConnectionModel> GetConnection()
        {
            // TODO: Implement logic
            return Ok(new XboxConnectionModel());
        }

        [HttpGet("get-login-consecutive-days")]
        public ActionResult<XboxLoginConsecutiveDaysResponse> GetXboxUserLoginConsecutiveDays()
        {
            // TODO: Implement logic
            return Ok(new XboxLoginConsecutiveDaysResponse());
        }

        [HttpPost("disconnect")]
        public ActionResult<ApiSuccessResponse> Disconnect()
        {
            // TODO: Implement logic
            return Ok(new ApiSuccessResponse());
        }

        [HttpPost("translate")]
        public ActionResult<XboxCollectionsOfUserResponse> TranslateGamerTags([FromBody] XboxTranslateRequest xboxTranslateRequest)
        {
            // TODO: Implement logic
            return Ok(new XboxCollectionsOfUserResponse());
        }
    }
}
