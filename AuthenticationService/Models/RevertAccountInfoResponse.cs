namespace AuthenticationService.Models
{
    public class RevertAccountInfoResponse
    {
        public bool IsTwoStepVerificationEnabled { get; set; }
        public bool IsEmailVerified { get; set; }
        public bool IsEmailChanged { get; set; }
        public bool IsPhoneVerified { get; set; }
        public long UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Ticket { get; set; } = string.Empty;
    }
}
