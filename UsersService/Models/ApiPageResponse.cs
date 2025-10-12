using System.Collections.Generic;

using System.Collections.Generic;

namespace UsersService.Models
{
    public class ApiPageResponse<T>
    {
        public string PreviousPageCursor { get; set; } = string.Empty;
        public string NextPageCursor { get; set; } = string.Empty;
        public List<T> Data { get; set; } = new List<T>();
    }
}
