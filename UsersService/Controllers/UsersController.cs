using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using Shared.Data.Data;
using Shared.Data.Entities;
using UsersService.Models;

namespace UsersService.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/users")]
    public class UsersController : ControllerBase
    {
        private readonly UsersDbContext _context;

        public UsersController(UsersDbContext context)
        {
            _context = context;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<GetUserResponse>> GetUserById([FromRoute] long userId)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                return NotFound(new ErrorResponse
                {
                    Errors = new List<Error>
                    {
                        new Error { Code = 3, Message = "The user id is invalid." }
                    }
                });
            }

            var response = new GetUserResponse
            {
                Id = user.Id,
                Name = user.Name,
                DisplayName = user.DisplayName,
                Description = user.Description,
                Created = user.Created,
                IsBanned = user.IsBanned,
                HasVerifiedBadge = user.HasVerifiedBadge
            };

            return Ok(response);
        }

        [HttpGet("authenticated")]
        public ActionResult<AuthenticatedGetUserResponse> GetAuthenticatedUser()
        {
            // TODO: Implement logic
            return Ok(new AuthenticatedGetUserResponse());
        }

        [HttpGet("authenticated/age-bracket")]
        public ActionResult<UserAgeBracketResponse> GetAuthenticatedUserAgeBracket()
        {
            // TODO: Implement logic
            return Ok(new UserAgeBracketResponse());
        }

        [HttpGet("authenticated/country-code")]
        public ActionResult<UserCountryCodeResponse> GetAuthenticatedUserCountryCode()
        {
            // TODO: Implement logic
            return Ok(new UserCountryCodeResponse());
        }

        [HttpGet("authenticated/roles")]
        public ActionResult<UserRolesResponse> GetAuthenticatedUserRoles()
        {
            // TODO: Implement logic
            return Ok(new UserRolesResponse());
        }

        [HttpPost]
        [Route("~/v{version:apiVersion}/usernames/users")] // Note: Route override to match spec
        public async Task<ActionResult<ApiArrayResponse<MultiGetUserByNameResponse>>> GetUsersByUsernames([FromBody] MultiGetByUsernameRequest request)
        {
            if (request?.Usernames == null || !request.Usernames.Any())
            {
                return Ok(new ApiArrayResponse<MultiGetUserByNameResponse> { Data = new List<MultiGetUserByNameResponse>() });
            }

            var lowerCaseUsernames = request.Usernames.Select(u => u.ToLowerInvariant()).ToList();

            var users = await _context.Users
                .Where(u => lowerCaseUsernames.Contains(u.Name.ToLowerInvariant()))
                .ToListAsync();

            // banned users should be excluded if requested
            if (request.ExcludeBannedUsers)
            {
                users = users.Where(u => !u.IsBanned).ToList();
            }

            var responseData = users.Select(u => new MultiGetUserByNameResponse
            {
                RequestedUsername = request.Usernames.First(ru => ru.Equals(u.Name, StringComparison.InvariantCultureIgnoreCase)), // finding the original casing
                Id = u.Id,
                Name = u.Name,
                DisplayName = u.DisplayName,
                HasVerifiedBadge = u.HasVerifiedBadge
            }).ToList();

            return Ok(new ApiArrayResponse<MultiGetUserByNameResponse> { Data = responseData });
        }

        [HttpPost]
        public async Task<ActionResult<ApiArrayResponse<MultiGetUserResponse>>> GetUsersByIds([FromBody] MultiGetByUserIdRequest request)
        {
            if (request?.UserIds == null || !request.UserIds.Any())
            {
                return Ok(new ApiArrayResponse<MultiGetUserResponse> { Data = new List<MultiGetUserResponse>() });
            }

            var users = await _context.Users
                .Where(u => request.UserIds.Contains(u.Id))
                .ToListAsync();

            // banned users should be excluded if requested
            if (request.ExcludeBannedUsers)
            {
                users = users.Where(u => !u.IsBanned).ToList();
            }

            var responseData = users.Select(u => new MultiGetUserResponse
            {
                Id = u.Id,
                Name = u.Name,
                DisplayName = u.DisplayName,
                HasVerifiedBadge = u.HasVerifiedBadge
            }).ToList();

            return Ok(new ApiArrayResponse<MultiGetUserResponse> { Data = responseData });
        }
    }
}
