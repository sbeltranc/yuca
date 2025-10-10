using System;

namespace AuthenticationService.Models
{
    public class SignupRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime Birthday { get; set; }
        public int Gender { get; set; }
    }
}
