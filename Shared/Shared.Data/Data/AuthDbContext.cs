using Microsoft.EntityFrameworkCore;
using Shared.Data.Entities;

namespace Shared.Data.Data
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        public DbSet<UserCredential> UserCredentials { get; set; }
    }
}
