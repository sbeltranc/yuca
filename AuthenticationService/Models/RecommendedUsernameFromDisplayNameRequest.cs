using System;

namespace AuthenticationService.Models
{
    public class RecommendedUsernameFromDisplayNameRequest
    {
        public string DisplayName { get; set; }
        public DateTime BirthDay { get; set; }
    }
}
