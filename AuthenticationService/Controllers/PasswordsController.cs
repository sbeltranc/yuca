using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using AuthenticationService.Models;
using Shared.Services;
using Shared.Data.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/passwords")]
    public class PasswordsController : ControllerBase
    {
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private readonly AuthDbContext _authDbContext;

        public PasswordsController(IAuthenticatedUserService authenticatedUserService, AuthDbContext authDbContext)
        {
            _authenticatedUserService = authenticatedUserService;
            _authDbContext = authDbContext;
        }

        [HttpPost("change")]
        public async Task<IActionResult> ChangePassword([FromBody] PasswordChangeModel request)
        {
            var user = await _authenticatedUserService.GetCurrentUserAsync();
            if (user == null)
            {
                return Unauthorized();
            }

            var userCredential = await _authDbContext.UserCredentials.FirstOrDefaultAsync(uc => uc.UserId == user.Id);
            if (userCredential == null || !BCrypt.Net.BCrypt.Verify(request.CurrentPassword, userCredential.PasswordHash))
            {
                return BadRequest(new { message = "Invalid current password" });
            }

            userCredential.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);

            var securityTokens = await _authDbContext.SecurityTokens.Where(st => st.UserId == user.Id).ToListAsync();
            _authDbContext.SecurityTokens.RemoveRange(securityTokens);

            await _authDbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("validate")]
        public ActionResult<PasswordValidationResponse> ValidateFromBody([FromBody] PasswordValidationModel request)
        {
            var (isValid, message) = PasswordValidator.Validate(request.Password);
            if (!isValid)
            {
                return BadRequest(new PasswordValidationResponse { Code = 1, Message = message });
            }
            return Ok(new PasswordValidationResponse { Code = 0, Message = message });
        }

        [HttpGet("validate")]
        public ActionResult<PasswordValidationResponse> ValidateFromUri([FromQuery] string username, [FromQuery] string password)
        {
            var (isValid, message) = PasswordValidator.Validate(password);
            if (!isValid)
            {
                return BadRequest(new PasswordValidationResponse { Code = 1, Message = message });
            }
            return Ok(new PasswordValidationResponse { Code = 0, Message = message });
        }
    }
}
