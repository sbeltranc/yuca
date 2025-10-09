namespace UsersService.Models
{
    public class MultiGetUserByNameResponse
    {
        public string RequestedUsername { get; set; }
        public bool HasVerifiedBadge { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
}
