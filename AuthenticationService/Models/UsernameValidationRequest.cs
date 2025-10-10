using System;

namespace AuthenticationService.Models
{
    public class UsernameValidationRequest
    {
        public string Username { get; set; }
        public DateTime Birthday { get; set; }
        public int Context { get; set; }
    }
}
