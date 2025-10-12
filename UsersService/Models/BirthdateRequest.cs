namespace UsersService.Models
{
    public class BirthdateRequest
    {
        public int BirthMonth { get; set; }
        public int BirthDay { get; set; }
        public int BirthYear { get; set; }
        public string Password { get; set; } = string.Empty;
    }
}
