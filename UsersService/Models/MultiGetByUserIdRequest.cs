using System.Collections.Generic;

namespace UsersService.Models
{
    public class MultiGetByUserIdRequest
    {
        public List<long> UserIds { get; set; }
        public bool ExcludeBannedUsers { get; set; }
    }
}
