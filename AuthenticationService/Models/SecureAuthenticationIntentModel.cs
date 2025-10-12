namespace AuthenticationService.Models
{
    public class SecureAuthenticationIntentModel
    {
        public string ClientPublicKey { get; set; } = string.Empty;
        public int ClientVersion { get; set; }
        public string SaiSignature { get; set; } = string.Empty;
        public string ServerNonce { get; set; } = string.Empty;
    }
}
