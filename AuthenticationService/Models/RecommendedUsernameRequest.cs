using System;

namespace AuthenticationService.Models
{
    public class RecommendedUsernameRequest
    {
        public string Username { get; set; }
        public DateTime BirthDay { get; set; }
    }
}
