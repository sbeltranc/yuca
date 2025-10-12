using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using AuthenticationService.Models;
using Shared.Data.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shared.Data.Entities;
using System;
using System.Collections.Generic;
using Shared.Services;
using AuditService.Services;

namespace AuthenticationService.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/account/pin")]
    public class AccountPinController : ControllerBase
    {
        private readonly AuthDbContext _context;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private readonly IAuditService _auditService;

        public AccountPinController(AuthDbContext context, IAuthenticatedUserService authenticatedUserService, IAuditService auditService)
        {
            _context = context;
            _authenticatedUserService = authenticatedUserService;
            _auditService = auditService;
        }

        [HttpGet]
        public async Task<ActionResult<AccountPinStatusResponse>> GetAccountPinStatus()
        {
            var user = await _authenticatedUserService.GetCurrentUserAsync();
            if (user == null)
            {
                return Unauthorized(new ErrorResponse { Errors = new List<Error> { new Error { Code = 0, Message = "Authorization has been denied for this request." } } });
            }

            var userCredential = await _context.UserCredentials.FirstOrDefaultAsync(uc => uc.UserId == user.Id);

            if (userCredential == null)
            {
                return NotFound();
            }

            return Ok(new AccountPinStatusResponse { IsEnabled = !string.IsNullOrEmpty(userCredential.Pin), UnlockedUntil = userCredential.PinUnlockedUntil.HasValue ? (long?)((DateTimeOffset)userCredential.PinUnlockedUntil.Value).ToUnixTimeSeconds() : null });
        }

        [HttpPost]
        public async Task<ActionResult<ApiSuccessResponse>> NewAccountPin([FromBody] AccountPinRequest request)
        {
            var user = await _authenticatedUserService.GetCurrentUserAsync();
            if (user == null)
            {
                return Unauthorized(new ErrorResponse { Errors = new List<Error> { new Error { Code = 0, Message = "Authorization has been denied for this request." } } });
            }

            var userCredential = await _context.UserCredentials.FirstOrDefaultAsync(uc => uc.UserId == user.Id);

            if (userCredential == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(userCredential.Pin))
            {
                return BadRequest(new ErrorResponse { Errors = new List<Error> { new Error { Code = 1, Message = "Account PIN already exists." } } });
            }

            userCredential.Pin = BCrypt.Net.BCrypt.HashPassword(request.Pin);
            await _context.SaveChangesAsync();

            await _auditService.AddRecordAsync(user.Id, HttpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty, "accountpin.create", new { Success = true });

            return Ok(new ApiSuccessResponse { Success = true });
        }

        [HttpPatch]
        public async Task<ActionResult<ApiSuccessResponse>> UpdateAccountPin([FromBody] AccountPinRequest request)
        {
            var user = await _authenticatedUserService.GetCurrentUserAsync();
            if (user == null)
            {
                return Unauthorized(new ErrorResponse { Errors = new List<Error> { new Error { Code = 0, Message = "Authorization has been denied for this request." } } });
            }

            var userCredential = await _context.UserCredentials.FirstOrDefaultAsync(uc => uc.UserId == user.Id);

            if (userCredential == null || string.IsNullOrEmpty(userCredential.Pin))
            {
                return BadRequest(new ErrorResponse { Errors = new List<Error> { new Error { Code = 2, Message = "Account PIN not set." } } });
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Pin, userCredential.Pin))
            {
                await _auditService.AddRecordAsync(user.Id, HttpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty, "accountpin.update.attempt", new { Success = false, Reason = "InvalidPin" });
                return BadRequest(new ErrorResponse { Errors = new List<Error> { new Error { Code = 3, Message = "Invalid PIN." } } });
            }

            userCredential.Pin = BCrypt.Net.BCrypt.HashPassword(request.Pin);
            await _context.SaveChangesAsync();

            await _auditService.AddRecordAsync(user.Id, HttpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty, "accountpin.update", new { Success = true });

            return Ok(new ApiSuccessResponse { Success = true });
        }

        [HttpDelete]
        public async Task<ActionResult<ApiSuccessResponse>> DeleteAccountPin([FromBody] AccountPinRequest request)
        {
            var user = await _authenticatedUserService.GetCurrentUserAsync();
            if (user == null)
            {
                return Unauthorized(new ErrorResponse { Errors = new List<Error> { new Error { Code = 0, Message = "Authorization has been denied for this request." } } });
            }

            var userCredential = await _context.UserCredentials.FirstOrDefaultAsync(uc => uc.UserId == user.Id);

            if (userCredential == null || string.IsNullOrEmpty(userCredential.Pin))
            {
                return BadRequest(new ErrorResponse { Errors = new List<Error> { new Error { Code = 2, Message = "Account PIN not set." } } });
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Pin, userCredential.Pin))
            {
                await _auditService.AddRecordAsync(user.Id, HttpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty, "accountpin.delete.attempt", new { Success = false, Reason = "InvalidPin" });
                return Forbid();
            }

            userCredential.Pin = null;
            await _context.SaveChangesAsync();

            await _auditService.AddRecordAsync(user.Id, HttpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty, "accountpin.delete", new { Success = true });

            return Ok(new ApiSuccessResponse { Success = true });
        }

        [HttpPost("lock")]
        public async Task<ActionResult<ApiSuccessResponse>> LockAccountPin()
        {
            var user = await _authenticatedUserService.GetCurrentUserAsync();
            if (user == null)
            {
                return Unauthorized(new ErrorResponse { Errors = new List<Error> { new Error { Code = 0, Message = "Authorization has been denied for this request." } } });
            }

            var userCredential = await _context.UserCredentials.FirstOrDefaultAsync(uc => uc.UserId == user.Id);

            if (userCredential != null)
            {
                userCredential.PinUnlockedUntil = null;
                await _context.SaveChangesAsync();
            }

            await _auditService.AddRecordAsync(user.Id, HttpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty, "accountpin.lock", new { Success = true });

            return Ok(new ApiSuccessResponse { Success = true });
        }

        [HttpPost("unlock")]
        public async Task<ActionResult<AccountPinResponse>> UnlockAccountPin([FromBody] AccountPinRequest request)
        {
            var user = await _authenticatedUserService.GetCurrentUserAsync();
            if (user == null)
            {
                return Unauthorized(new ErrorResponse { Errors = new List<Error> { new Error { Code = 0, Message = "Authorization has been denied for this request." } } });
            }

            var userCredential = await _context.UserCredentials.FirstOrDefaultAsync(uc => uc.UserId == user.Id);

            if (userCredential == null || string.IsNullOrEmpty(userCredential.Pin))
            {
                return BadRequest(new ErrorResponse { Errors = new List<Error> { new Error { Code = 2, Message = "Account PIN not set." } } });
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Pin, userCredential.Pin))
            {
                await _auditService.AddRecordAsync(user.Id, HttpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty, "accountpin.unlock.attempt", new { Success = false, Reason = "InvalidPin" });
                return Forbid();
            }

            userCredential.PinUnlockedUntil = DateTime.UtcNow.AddMinutes(15);
            await _context.SaveChangesAsync();

            await _auditService.AddRecordAsync(user.Id, HttpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty, "accountpin.unlock", new { Success = true });

            return Ok(new AccountPinResponse { UnlockedUntil = userCredential.PinUnlockedUntil.HasValue ? (long?)((DateTimeOffset)userCredential.PinUnlockedUntil.Value).ToUnixTimeSeconds() : null });
        }
    }
}
