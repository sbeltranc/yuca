using System.Threading.Tasks;

namespace AuditService.Services
{
    public interface IAuditService
    {
        Task AddRecordAsync(long? userId, string ipAddress, string action, object details);
    }
}
