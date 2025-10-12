namespace AuthenticationService.Models
{
    public class LogoutFromAllSessionsAndReauthenticateRequest
    {
        public SecureAuthenticationIntentModel SecureAuthenticationIntent { get; set; } = new SecureAuthenticationIntentModel();
    }
}
