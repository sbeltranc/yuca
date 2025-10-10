using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Shared.Data.Data;
using AuthenticationService.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shared.Data.Entities;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using AuditService.Services;

namespace AuthenticationService.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}")]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthDbContext _authDbContext;
        private readonly UsersDbContext _usersDbContext;
        private readonly IAuditService _auditService;

        public AuthenticationController(AuthDbContext authDbContext, UsersDbContext usersDbContext, IAuditService auditService)
        {
            _authDbContext = authDbContext;
            _usersDbContext = usersDbContext;
            _auditService = auditService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
        {
            User user = null;
            switch (request.ctype)
            {
                case "Username":
                    user = await _usersDbContext.Users.FirstOrDefaultAsync(u => u.Name == request.cvalue);
                    break;
                case "Email":
                    // not implementing yet cuz.. i dont store emails yet
                    break;
            }

            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty;

            if (user == null)
            {
                await _auditService.AddRecordAsync(null, ipAddress, "LoginAttempt", new { Ctype = request.ctype, Cvalue = request.cvalue, Success = false });
                return Unauthorized(new ErrorResponse { Errors = new System.Collections.Generic.List<Error> { new Error { Code = 1, Message = "Incorrect username or password. Please try again." } } });
            }

            var userCredential = await _authDbContext.UserCredentials.FirstOrDefaultAsync(uc => uc.UserId == user.Id);

            if (userCredential == null || !BCrypt.Net.BCrypt.Verify(request.password, userCredential.PasswordHash))
            {
                await _auditService.AddRecordAsync(user.Id, ipAddress, "LoginAttempt", new { Ctype = request.ctype, Cvalue = request.cvalue, Success = false });
                return Unauthorized(new ErrorResponse { Errors = new System.Collections.Generic.List<Error> { new Error { Code = 1, Message = "Incorrect username or password. Please try again." } } });
            }

            await _auditService.AddRecordAsync(user.Id, ipAddress, "LoginAttempt", new { Ctype = request.ctype, Cvalue = request.cvalue, Success = true });

            var securityToken = GenerateSecurityToken();
            Response.Cookies.Append(".ROBLOSECURITY", securityToken, new CookieOptions { HttpOnly = true, Secure = true, SameSite = SameSiteMode.Lax });

            return Ok(new LoginResponse
            {
                User = new SkinnyUserResponse
                {
                    Id = user.Id,
                    Name = user.Name,
                    DisplayName = user.DisplayName
                },
                IsBanned = user.IsBanned
            });
        }

        [HttpPost("signup")]
        public async Task<ActionResult<SignupResponse>> Signup([FromBody] SignupRequest request)
        {
            if (await _usersDbContext.Users.AnyAsync(u => u.Name == request.Username))
            {
                return BadRequest(new ErrorResponse { Errors = new System.Collections.Generic.List<Error> { new Error { Code = 6, Message = "Username already taken." } } });
            }

            var user = new User
            {
                Name = request.Username,
                DisplayName = request.Username,
                Created = DateTime.UtcNow
            };

            _usersDbContext.Users.Add(user);
            await _usersDbContext.SaveChangesAsync();

            var userCredential = new UserCredential
            {
                UserId = user.Id,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            _authDbContext.UserCredentials.Add(userCredential);
            await _authDbContext.SaveChangesAsync();

            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
            await _auditService.AddRecordAsync(user.Id, ipAddress, "Signup", new { Username = request.Username });

            return Ok(new SignupResponse { UserId = user.Id });
        }

        [HttpGet("auth/metadata")]
        public ActionResult<AuthMetaDataResponse> GetMetaData()
        {
            return Ok(new AuthMetaDataResponse { CookieLawNoticeTimeout = 30 });
        }

        private string GenerateSecurityToken()
        {
            using (var randomNumberGenerator = RandomNumberGenerator.Create())
            {
                var randomBytes = new byte[256];
                randomNumberGenerator.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
        }
    }
}
