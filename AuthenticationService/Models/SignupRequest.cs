using System;

namespace AuthenticationService.Models
{
    public class SignupRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime Birthday { get; set; }
        public int Gender { get; set; }
    }
}
