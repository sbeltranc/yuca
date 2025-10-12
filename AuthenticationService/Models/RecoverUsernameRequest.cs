namespace AuthenticationService.Models
{
    public class RecoverUsernameRequest
    {
        public int TargetType { get; set; }
        public string Target { get; set; } = string.Empty;
    }
}
