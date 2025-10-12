using System.Collections.Generic;

using System.Collections.Generic;

namespace UsersService.Models
{
    public class ApiArrayResponse<T>
    {
        public List<T> Data { get; set; } = new List<T>();
    }
}
