namespace AuthenticationService.Models
{
    public class AccountPinStatusResponse
    {
        public bool IsEnabled { get; set; }
        public long? UnlockedUntil { get; set; }
    }
}
