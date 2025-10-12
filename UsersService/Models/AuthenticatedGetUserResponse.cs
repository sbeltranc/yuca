namespace UsersService.Models
{
    public class AuthenticatedGetUserResponse
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
    }
}
