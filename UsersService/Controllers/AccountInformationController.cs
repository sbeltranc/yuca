using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using UsersService.Models;
using UsersService.Data;

namespace UsersService.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}")]
    public class AccountInformationController : ControllerBase
    {
        private readonly UsersDbContext _context;

        public AccountInformationController(UsersDbContext context)
        {
            _context = context;
        }
        [HttpGet("birthdate")]
        public ActionResult<BirthdateResponse> GetBirthdate()
        {
            // TODO: Implement logic
            return Ok(new BirthdateResponse());
        }

        [HttpPost("birthdate")]
        public IActionResult UpdateBirthdate([FromBody] BirthdateRequest request)
        {
            // TODO: Implement logic
            return Ok();
        }

        [HttpGet("description")]
        public ActionResult<DescriptionResponse> GetDescription()
        {
            // TODO: Implement logic
            return Ok(new DescriptionResponse());
        }

        [HttpPost("description")]
        public ActionResult<DescriptionResponse> UpdateDescription([FromBody] DescriptionRequest request)
        {
            // TODO: Implement logic
            return Ok(new DescriptionResponse());
        }

        [HttpGet("gender")]
        public ActionResult<GenderResponse> GetGender()
        {
            // TODO: Implement logic
            return Ok(new GenderResponse());
        }

        [HttpPost("gender")]
        public IActionResult UpdateGender([FromBody] GenderRequest request)
        {
            // TODO: Implement logic
            return Ok();
        }
    }
}
