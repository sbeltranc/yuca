using System.Collections.Generic;

namespace UsersService.Models
{
    public class SearchGetUserResponse
    {
        public List<string> PreviousUsernames { get; set; }
        public bool HasVerifiedBadge { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
}
