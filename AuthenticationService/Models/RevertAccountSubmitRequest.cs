namespace AuthenticationService.Models
{
    public class RevertAccountSubmitRequest
    {
        public long UserId { get; set; }
        public string NewPassword { get; set; }
        public string NewPasswordRepeated { get; set; }
        public string Ticket { get; set; }
        public string TwoStepVerificationChallengeId { get; set; }
        public string TwoStepVerificationToken { get; set; }
    }
}
