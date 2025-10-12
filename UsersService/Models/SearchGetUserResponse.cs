using System.Collections.Generic;

using System.Collections.Generic;

namespace UsersService.Models
{
    public class SearchGetUserResponse
    {
        public List<string> PreviousUsernames { get; set; } = new List<string>();
        public bool HasVerifiedBadge { get; set; }
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
    }
}
