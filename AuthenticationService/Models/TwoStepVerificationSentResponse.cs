namespace AuthenticationService.Models
{
    public class TwoStepVerificationSentResponse
    {
        public int mediaType { get; set; }
        public string ticket { get; set; }
    }
}
