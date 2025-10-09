namespace AuthenticationService.Models
{
    public class TwoStepVerificationSentResponse
    {
        public int MediaType { get; set; }
        public string Ticket { get; set; }
    }
}
