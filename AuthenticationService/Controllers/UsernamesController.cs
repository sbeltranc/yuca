using Microsoft.AspNetCore.Mvc;
using AuthenticationService.Models;
using System;
using Shared.Data.Data;
using Shared.Data.Entities;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Asp.Versioning;
using System.Text.RegularExpressions;

namespace AuthenticationService.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/usernames")]
    public class UsernamesController : ControllerBase
    {
        private readonly UsersDbContext _usersDbContext;

        public UsernamesController(UsersDbContext usersDbContext)
        {
            _usersDbContext = usersDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<UsernamesResponse>> Get([FromQuery] string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return Ok(new UsernamesResponse { Usernames = new List<string>() });
            }

            var usernames = username.Split(',');
            var existingUsernames = await _usersDbContext.Users
                .Where(u => usernames.Equals(u.Name)) // it's equals cause, that's how it works on the roblox api.. for some reason
                .Select(u => u.Name)
                .ToListAsync();

            return Ok(new UsernamesResponse { Usernames = existingUsernames });
        }

        [HttpGet("validate")]
        public ActionResult<UsernameValidationResponse> ValidateFromUri([FromQuery] string username, [FromQuery] DateTime birthday, [FromQuery] int context)
        {
            // TODO: Implement logic
            return Ok(new UsernameValidationResponse());
        }

        [HttpPost("validate")]
        public ActionResult<UsernameValidationResponse> ValidateFromBody([FromBody] UsernameValidationRequest request)
        {
            // TODO: Implement logic
            return Ok(new UsernameValidationResponse());
        }

        [HttpPost("recover")]
        public ActionResult<RecoverUsernameResponse> RecoverUsername([FromBody] RecoverUsernameRequest request)
        {
            // TODO: Implement logic
            return Ok(new RecoverUsernameResponse());
        }
    }
}
