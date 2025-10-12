using System.Collections.Generic;

using System.Collections.Generic;

namespace UsersService.Models
{
    public class MultiGetByUserIdRequest
    {
        public List<long> UserIds { get; set; } = new List<long>();
        public bool ExcludeBannedUsers { get; set; }
    }
}
