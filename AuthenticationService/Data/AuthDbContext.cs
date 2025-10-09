using Microsoft.EntityFrameworkCore;
using AuthenticationService.Entities;

namespace AuthenticationService.Data
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        public DbSet<UserCredential> UserCredentials { get; set; }
    }
}
