namespace AuthenticationService.Models
{
    public class AccountPinStatusResponse
    {
        public bool IsEnabled { get; set; }
        public double? UnlockedUntil { get; set; }
    }
}
