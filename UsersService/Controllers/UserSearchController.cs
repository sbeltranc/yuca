using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using UsersService.Models;
using UsersService.Data;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace UsersService.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/users")]
    public class UserSearchController : ControllerBase
    {
        private readonly UsersDbContext _context;

        public UserSearchController(UsersDbContext context)
        {
            _context = context;
        }
        [HttpGet("search")]
        public async Task<ActionResult<ApiPageResponse<SearchGetUserResponse>>> SearchUsers(
            [FromQuery] string keyword,
            [FromQuery] string sessionId = null,
            [FromQuery] int limit = 10,
            [FromQuery] string cursor = null)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = new List<Error> { new Error { Code = 6, Message = "The keyword is too short." } }
                });
            }

            var query = _context.Users
                .Where(u => u.Name.ToLower().Contains(keyword.ToLower()) || u.DisplayName.ToLower().Contains(keyword.ToLower()))
                .OrderBy(u => u.Id);

            if (!string.IsNullOrEmpty(cursor) && long.TryParse(cursor, out long cursorId))
            {
                query = query.Where(u => u.Id > cursorId);
            }

            var users = await query.Take(limit).ToListAsync();

            string nextCursor = null;
            if (users.Any() && users.Count == limit)
            {
                nextCursor = users.Last().Id.ToString();
            }

            var responseData = users.Select(u => new SearchGetUserResponse
            {
                Id = u.Id,
                Name = u.Name,
                DisplayName = u.DisplayName,
                HasVerifiedBadge = u.HasVerifiedBadge,
                PreviousUsernames = new List<string>() // TODO: implement username changes
            }).ToList();

            return Ok(new ApiPageResponse<SearchGetUserResponse>
            {
                Data = responseData,
                NextPageCursor = nextCursor
            });
        }
    }
}
