namespace AuthenticationService.Models
{
    public class RevertAccountSubmitRequest
    {
        public long UserId { get; set; }
        public string NewPassword { get; set; } = string.Empty;
        public string NewPasswordRepeated { get; set; } = string.Empty;
        public string Ticket { get; set; } = string.Empty;
        public string TwoStepVerificationChallengeId { get; set; } = string.Empty;
        public string TwoStepVerificationToken { get; set; } = string.Empty;
    }
}
