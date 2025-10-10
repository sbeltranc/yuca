using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using AuthenticationService.Models;
using Shared.Data.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shared.Data.Entities;
using System;
using System.Collections.Generic;

namespace AuthenticationService.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/account/pin")]
    public class AccountPinController : ControllerBase
    {
        private readonly AuthDbContext _context;
        private const long HardcodedUserId = 1; // Hardcoded user ID for now

        public AccountPinController(AuthDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<AccountPinStatusResponse>> GetAccountPinStatus()
        {
            var userCredential = await _context.UserCredentials.FirstOrDefaultAsync(uc => uc.UserId == HardcodedUserId);

            if (userCredential == null)
            {
                return NotFound();
            }

            return Ok(new AccountPinStatusResponse { IsEnabled = !string.IsNullOrEmpty(userCredential.Pin), UnlockedUntil = userCredential.PinUnlockedUntil.HasValue ? (long?)((DateTimeOffset)userCredential.PinUnlockedUntil.Value).ToUnixTimeSeconds() : null });
        }

        [HttpPost]
        public async Task<ActionResult<ApiSuccessResponse>> NewAccountPin([FromBody] AccountPinRequest request)
        {
            var userCredential = await _context.UserCredentials.FirstOrDefaultAsync(uc => uc.UserId == HardcodedUserId);

            if (userCredential == null)
            {
                // For simplicity, creating a new credential if one doesn't exist.
                // In a real scenario, this should be handled as part of user registration.
                userCredential = new UserCredential { UserId = HardcodedUserId, PasswordHash = "" }; // Empty password hash for now
                _context.UserCredentials.Add(userCredential);
            }

            if (!string.IsNullOrEmpty(userCredential.Pin))
            {
                return BadRequest(new ErrorResponse { Errors = new List<Error> { new Error { Code = 1, Message = "Account PIN already exists." } } });
            }

            userCredential.Pin = request.Pin; // Storing as plain text for now
            await _context.SaveChangesAsync();

            return Ok(new ApiSuccessResponse { Success = true });
        }

        [HttpPatch]
        public async Task<ActionResult<ApiSuccessResponse>> UpdateAccountPin([FromBody] AccountPinRequest request)
        {
            var userCredential = await _context.UserCredentials.FirstOrDefaultAsync(uc => uc.UserId == HardcodedUserId);

            if (userCredential == null || string.IsNullOrEmpty(userCredential.Pin))
            {
                return BadRequest(new ErrorResponse { Errors = new List<Error> { new Error { Code = 2, Message = "Account PIN not set." } } });
            }

            userCredential.Pin = request.Pin; // Storing as plain text for now
            await _context.SaveChangesAsync();

            return Ok(new ApiSuccessResponse { Success = true });
        }

        [HttpDelete]
        public async Task<ActionResult<ApiSuccessResponse>> DeleteAccountPin([FromBody] AccountPinRequest request)
        {
            var userCredential = await _context.UserCredentials.FirstOrDefaultAsync(uc => uc.UserId == HardcodedUserId);

            if (userCredential == null || string.IsNullOrEmpty(userCredential.Pin))
            {
                return BadRequest(new ErrorResponse { Errors = new List<Error> { new Error { Code = 2, Message = "Account PIN not set." } } });
            }

            if (userCredential.Pin != request.Pin)
            {
                return Forbid();
            }

            userCredential.Pin = null;
            await _context.SaveChangesAsync();

            return Ok(new ApiSuccessResponse { Success = true });
        }

        [HttpPost("lock")]
        public async Task<ActionResult<ApiSuccessResponse>> LockAccountPin()
        {
            var userCredential = await _context.UserCredentials.FirstOrDefaultAsync(uc => uc.UserId == HardcodedUserId);

            if (userCredential != null)
            {
                userCredential.PinUnlockedUntil = null;
                await _context.SaveChangesAsync();
            }

            return Ok(new ApiSuccessResponse { Success = true });
        }

        [HttpPost("unlock")]
        public async Task<ActionResult<AccountPinResponse>> UnlockAccountPin([FromBody] AccountPinRequest request)
        {
            var userCredential = await _context.UserCredentials.FirstOrDefaultAsync(uc => uc.UserId == HardcodedUserId);

            if (userCredential == null || string.IsNullOrEmpty(userCredential.Pin))
            {
                return BadRequest(new ErrorResponse { Errors = new List<Error> { new Error { Code = 2, Message = "Account PIN not set." } } });
            }

            if (userCredential.Pin != request.Pin)
            {
                return Forbid();
            }

            userCredential.PinUnlockedUntil = DateTime.UtcNow.AddMinutes(15);
            await _context.SaveChangesAsync();

            return Ok(new AccountPinResponse { UnlockedUntil = userCredential.PinUnlockedUntil.HasValue ? (long?)((DateTimeOffset)userCredential.PinUnlockedUntil.Value).ToUnixTimeSeconds() : null });
        }
    }
}
