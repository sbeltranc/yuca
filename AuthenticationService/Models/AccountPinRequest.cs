namespace AuthenticationService.Models
{
    public class AccountPinRequest
    {
        public string Pin { get; set; }
        public string ReauthenticationToken { get; set; }
    }
}
