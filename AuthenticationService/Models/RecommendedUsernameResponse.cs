using System.Collections.Generic;

namespace AuthenticationService.Models
{
    public class RecommendedUsernameResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public List<string> SuggestedUsernames { get; set; } = new List<string>();
    }
}
