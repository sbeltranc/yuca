using Microsoft.AspNetCore.Mvc;
using AuthenticationService.Models;
using Shared.Data.Data;
using Shared.Data.Entities;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using Asp.Versioning;

namespace AuthenticationService.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/validators")]
    public class ValidatorsController : ControllerBase
    {
        private readonly UsersDbContext _usersDbContext;

        public ValidatorsController(UsersDbContext usersDbContext)
        {
            _usersDbContext = usersDbContext;
        }

        [HttpGet("email")]
        public ActionResult<EmailValidationResponse> IsEmailValid([FromQuery] string email)
        {
            var isValid = Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
            return Ok(new EmailValidationResponse { IsEmailValid = isValid });
        }

        [HttpGet("recommendedUsernameFromDisplayName")]
        public async Task<ActionResult<RecommendedUsernameResponse>> GetRecommendedUsernameFromDisplayName([FromQuery] string displayName, [FromQuery] DateTime birthDay)
        {
            var username = displayName.Replace(" ", "");
            return await GetRecommendedUsername(username, birthDay);
        }

        [HttpPost("recommendedUsernameFromDisplayName")]
        public async Task<ActionResult<RecommendedUsernameResponse>> PostRecommendedUsernameFromDisplayName([FromBody] RecommendedUsernameFromDisplayNameRequest request)
        {
            var username = request.DisplayName.Replace(" ", "");
            return await GetRecommendedUsername(username, request.BirthDay);
        }

        [HttpGet("username")]
        public async Task<ActionResult<RecommendedUsernameResponse>> GetRecommendedUsername([FromQuery] string username, [FromQuery] DateTime birthDay)
        {
            var isTaken = await _usersDbContext.Users.AnyAsync(u => u.Name == username);
            if (!isTaken)
            {
                return Ok(new RecommendedUsernameResponse { SuggestedUsernames = new List<string> { username } });
            }

            var suggestedUsernames = new List<string>();
            for (int i = 1; i <= 3; i++)
            {
                var newUsername = $"{username}{i}";
                if (!await _usersDbContext.Users.AnyAsync(u => u.Name == newUsername))
                {
                    suggestedUsernames.Add(newUsername);
                }
            }

            return Ok(new RecommendedUsernameResponse { SuggestedUsernames = suggestedUsernames });
        }

        [HttpPost("username")]
        public async Task<ActionResult<RecommendedUsernameResponse>> PostRecommendedUsername([FromBody] RecommendedUsernameRequest request)
        {
            return await GetRecommendedUsername(request.Username, request.BirthDay);
        }
    }
}
