using System.Collections.Generic;

namespace AuthenticationService.Models
{
    public class RecommendedUsernameResponse
    {
        public bool DidGenerateNewUsername { get; set; }
        public List<string> SuggestedUsernames { get; set; }
    }
}
