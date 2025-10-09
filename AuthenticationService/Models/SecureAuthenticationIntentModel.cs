namespace AuthenticationService.Models
{
    public class SecureAuthenticationIntentModel
    {
        public string ClientPublicKey { get; set; }
        public long ClientEpochTimestamp { get; set; }
        public string SaiSignature { get; set; }
        public string ServerNonce { get; set; }
    }
}
