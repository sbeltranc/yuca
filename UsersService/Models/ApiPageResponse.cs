using System.Collections.Generic;

namespace UsersService.Models
{
    public class ApiPageResponse<T>
    {
        public string PreviousPageCursor { get; set; }
        public string NextPageCursor { get; set; }
        public List<T> Data { get; set; }
    }
}
