using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared.Data.Data;
using Shared.Data.Entities;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System;

namespace Shared.Services
{
    public class AuthenticatedUserService : IAuthenticatedUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthDbContext _authDbContext;
        private readonly UsersDbContext _usersDbContext;

        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor, AuthDbContext authDbContext, UsersDbContext usersDbContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _authDbContext = authDbContext;
            _usersDbContext = usersDbContext;
        }

        public async Task<User> GetCurrentUserAsync()
        {
            if (_httpContextAccessor.HttpContext.Request.Cookies.TryGetValue(".ROBLOSECURITY", out var token))
            {
                var securityToken = await _authDbContext.SecurityTokens.FirstOrDefaultAsync(t => t.Token == token);
                if (securityToken != null)
                {
                    return await _usersDbContext.Users.FindAsync(securityToken.UserId);
                }
            }
            return null;
        }

        public async Task<string> CreateSecurityTokenAsync(long userId)
        {
            var token = GenerateSecurityToken();
            var securityToken = new SecurityToken
            {
                Token = token,
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };
            _authDbContext.SecurityTokens.Add(securityToken);
            await _authDbContext.SaveChangesAsync();
            return token;
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
