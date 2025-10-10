using AuditService.Data;
using AuditService.Entities;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace AuditService.Services
{
    public class AuditService : IAuditService
    {
        private readonly AuditDbContext _context;

        public AuditService(AuditDbContext context)
        {
            _context = context;
        }

        public async Task AddRecordAsync(long? userId, string ipAddress, string action, object details)
        {
            var auditLog = new AuditLog
            {
                UserId = userId,
                Timestamp = DateTime.UtcNow,
                IpAddress = ipAddress,
                Metadata = JsonSerializer.Serialize(new { action, details })
            };

            _context.AuditLogs.Add(auditLog);
            await _context.SaveChangesAsync();
        }
    }
}
