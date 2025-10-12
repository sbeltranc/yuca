using System;

namespace AuthenticationService.Models
{
    public class UsernameValidationRequest
    {
        public string Username { get; set; } = string.Empty;
        public DateTime Birthday { get; set; }
        public int Context { get; set; }
    }
}
