using System;
using System.ComponentModel.DataAnnotations;

namespace Shared.Data.Entities
{
    public class SecurityToken
    {
        [Key]
        public string Token { get; set; } = string.Empty;
        public long UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
