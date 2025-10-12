using System.Collections.Generic;

namespace UsersService.Models
{
    public class ErrorResponse
    {
        public List<Error> Errors { get; set; } = new List<Error>();
    }
}
