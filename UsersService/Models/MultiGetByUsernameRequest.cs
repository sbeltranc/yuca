using System.Collections.Generic;

namespace UsersService.Models
{
    public class MultiGetByUsernameRequest
    {
        public List<string> Usernames { get; set; }
        public bool ExcludeBannedUsers { get; set; }
    }
}
