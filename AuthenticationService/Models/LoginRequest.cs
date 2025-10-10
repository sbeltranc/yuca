namespace AuthenticationService.Models
{
    public class LoginRequest
    {
        public string ctype { get; set; }
        public string cvalue { get; set; }
        public string password { get; set; }
        public long? userId { get; set; }
        public string captchaId { get; set; }
        public string captchaToken { get; set; }
        public string captchaProvider { get; set; }
        public string challengeId { get; set; }
    }
}
