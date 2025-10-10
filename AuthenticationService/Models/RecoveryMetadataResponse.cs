namespace AuthenticationService.Models
{
    public class RecoveryMetadataResponse
    {
        public bool IsOnPhone { get; set; }
        public int CodeLength { get; set; }
        public bool IsPhoneFeatureEnabledForUsername { get; set; }
        public bool IsPhoneFeatureEnabledForPassword { get; set; }
        public bool IsBedev2CaptchaEnabledForPasswordReset { get; set; }
        public bool IsUsernameRecoveryDeprecated { get; set; }
    }
}
