using Shared.Data.Entities;
using System.Threading.Tasks;

namespace Shared.Services
{
    public interface IAuthenticatedUserService
    {
        Task<User> GetCurrentUserAsync();
        Task<string> CreateSecurityTokenAsync(long userId);
    }
}
