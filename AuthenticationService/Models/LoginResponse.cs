namespace AuthenticationService.Models
{
    public class LoginResponse
    {
        public SkinnyUserResponse User { get; set; }
        public TwoStepVerificationSentResponse TwoStepVerificationData { get; set; }
        public string IdentityVerificationLoginTicket { get; set; }
        public bool IsBanned { get; set; }
        public string AccountBlob { get; set; }
        public bool ShouldUpdateEmail { get; set; }
        public string RecoveryEmail { get; set; }
        public bool PasskeyRegistrationSucceeded { get; set; }
    }
}
