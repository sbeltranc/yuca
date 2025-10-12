using System.Collections.Generic;

using System.Collections.Generic;

namespace UsersService.Models
{
    public class MultiGetByUsernameRequest
    {
        public List<string> Usernames { get; set; } = new List<string>();
        public bool ExcludeBannedUsers { get; set; }
    }
}
