namespace AuthenticationService.Models
{
    public class LoginRequest
    {
        public string ctype { get; set; } = string.Empty;
        public string cvalue { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public long? userId { get; set; }
        public string? captchaId { get; set; }
        public string? captchaToken { get; set; }
        public string? captchaProvider { get; set; }
        public string? challengeId { get; set; }
    }
}
