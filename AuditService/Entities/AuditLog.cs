using System;
using System.ComponentModel.DataAnnotations;

namespace AuditService.Entities
{
    public class AuditLog
    {
        [Key]
        public long Id { get; set; }
        public long? UserId { get; set; }
        public DateTime Timestamp { get; set; }
        [Required]
        public string IpAddress { get; set; }
        public string Metadata { get; set; }
    }
}
