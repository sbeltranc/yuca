namespace AuthenticationService.Models
{
    public class LoginRequest
    {
        public int Ctype { get; set; }
        public string Cvalue { get; set; }
        public string Password { get; set; }
        public long? UserId { get; set; }
        public string SecurityQuestionSessionId { get; set; }
        public string SecurityQuestionRedemptionToken { get; set; }
        public SecureAuthenticationIntentModel SecureAuthenticationIntent { get; set; }
        public string AccountBlob { get; set; }
        public AccountLinkParameters AccountLinkParameters { get; set; }
        public string CaptchaId { get; set; }
        public string CaptchaToken { get; set; }
        public string CaptchaProvider { get; set; }
        public string ChallengeId { get; set; }
    }
}
