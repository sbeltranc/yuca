using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using AuthenticationService.Models;
using AuthenticationService.Data;

namespace AuthenticationService.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/account/pin")]
    public class AccountPinController : ControllerBase
    {
        private readonly AuthDbContext _context;

        public AccountPinController(AuthDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<AccountPinStatusResponse> GetAccountPinStatus()
        {
            // TODO: Implement logic
            return Ok(new AccountPinStatusResponse());
        }

        [HttpPost]
        public ActionResult<ApiSuccessResponse> NewAccountPin([FromBody] AccountPinRequest request)
        {
            // TODO: Implement logic
            return Ok(new ApiSuccessResponse { Success = true });
        }

        [HttpPatch]
        public ActionResult<ApiSuccessResponse> UpdateAccountPin([FromBody] AccountPinRequest request)
        {
            // TODO: Implement logic
            return Ok(new ApiSuccessResponse { Success = true });
        }

        [HttpDelete]
        public ActionResult<ApiSuccessResponse> DeleteAccountPin([FromBody] AccountPinRequest request)
        {
            // TODO: Implement logic
            return Ok(new ApiSuccessResponse { Success = true });
        }

        [HttpPost("lock")]
        public ActionResult<ApiSuccessResponse> LockAccountPin()
        {
            // TODO: Implement logic
            return Ok(new ApiSuccessResponse { Success = true });
        }

        [HttpPost("unlock")]
        public ActionResult<AccountPinResponse> UnlockAccountPin([FromBody] AccountPinRequest request)
        {
            // TODO: Implement logic
            return Ok(new AccountPinResponse());
        }
    }
}
