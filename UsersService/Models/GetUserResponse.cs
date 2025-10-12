using System;

namespace UsersService.Models
{
    public class GetUserResponse
    {
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public bool IsBanned { get; set; }
        public string ExternalAppDisplayName { get; set; }
        public bool HasVerifiedBadge { get; set; }
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
    }
}
