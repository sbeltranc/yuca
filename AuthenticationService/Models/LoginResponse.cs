namespace AuthenticationService.Models
{
    public class LoginResponse
    {
        public SkinnyUserResponse User { get; set; } = new SkinnyUserResponse();
        public bool IsBanned { get; set; }
        public string AccountBlob { get; set; } = string.Empty;
        public bool ShouldUpdateEmail { get; set; }
        public string? RecoveryEmail { get; set; }
    }
}
