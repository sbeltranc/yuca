namespace UsersService.Models
{
    public class MultiGetUserByNameResponse
    {
        public string RequestedUsername { get; set; } = string.Empty;
        public long Id { get; set; }
        public bool HasVerifiedBadge { get; set; }
        public string Name { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
    }
}
