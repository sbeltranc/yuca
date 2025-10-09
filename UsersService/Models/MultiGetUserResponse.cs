namespace UsersService.Models
{
    public class MultiGetUserResponse
    {
        public bool HasVerifiedBadge { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
}
