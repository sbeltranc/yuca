using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using UsersService.Models;
using Shared.Data.Data;
using Shared.Services;
using Shared.Services.Cache;
using System.Threading.Tasks;

namespace UsersService.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/users/authenticated")]
    public class AccountInformationController : ControllerBase
    {
        private readonly UsersDbContext _context;
        private readonly IRedisCacheService _redisCacheService;
        private readonly IAuthenticatedUserService _authenticatedUserService;

        public AccountInformationController(UsersDbContext context, IRedisCacheService redisCacheService, IAuthenticatedUserService authenticatedUserService)
        {
            _context = context;
            _redisCacheService = redisCacheService;
            _authenticatedUserService = authenticatedUserService;
        }
        [HttpGet("birthdate")]
        public ActionResult<BirthdateResponse> GetBirthdate()
        {
            // TODO: Implement logic
            return Ok(new BirthdateResponse());
        }

        [HttpPost("birthdate")]
        public async Task<IActionResult> UpdateBirthdate([FromBody] BirthdateRequest request)
        {
            var user = await _authenticatedUserService.GetCurrentUserAsync();
            if (user == null)
            {
                return Unauthorized();
            }

            user.Birthdate = new System.DateTime(request.BirthYear, request.BirthMonth, request.BirthDay);
            await _context.SaveChangesAsync();

            var cacheKey = $"user:{user.Id}";
            await _redisCacheService.DeleteAsync(cacheKey);

            return Ok();
        }

        [HttpGet("description")]
        public ActionResult<DescriptionResponse> GetDescription()
        {
            // TODO: Implement logic
            return Ok(new DescriptionResponse());
        }

        [HttpPost("description")]
        public async Task<ActionResult<DescriptionResponse>> UpdateDescription([FromBody] DescriptionRequest request)
        {
            var user = await _authenticatedUserService.GetCurrentUserAsync();
            if (user == null)
            {
                return Unauthorized();
            }

            user.Description = request.Description;
            await _context.SaveChangesAsync();

            var cacheKey = $"user:{user.Id}";
            await _redisCacheService.DeleteAsync(cacheKey);

            return Ok(new DescriptionResponse
            {
                Description = user.Description
            });
        }

        [HttpGet("gender")]
        public ActionResult<GenderResponse> GetGender()
        {
            // TODO: Implement logic
            return Ok(new GenderResponse());
        }

        [HttpPost("gender")]
        public async Task<IActionResult> UpdateGender([FromBody] GenderRequest request)
        {
            var user = await _authenticatedUserService.GetCurrentUserAsync();
            if (user == null)
            {
                return Unauthorized();
            }

            user.Gender = (byte)request.Gender;
            await _context.SaveChangesAsync();

            var cacheKey = $"user:{user.Id}";
            await _redisCacheService.DeleteAsync(cacheKey);

            return Ok();
        }
    }
}
