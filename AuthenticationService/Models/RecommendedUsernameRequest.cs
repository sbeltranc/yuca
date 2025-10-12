using System;

namespace AuthenticationService.Models
{
    public class RecommendedUsernameRequest
    {
        public string Username { get; set; } = string.Empty;
        public DateTime BirthDay { get; set; }
    }
}
