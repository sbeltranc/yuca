using System.ComponentModel.DataAnnotations;

namespace Shared.Data.Entities
{
    public class UserCredential
    {
        [Key]
        public long Id { get; set; }

        // Foreign key to the User in the UsersService
        public long UserId { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public string? Pin { get; set; }

        public DateTime? PinUnlockedUntil { get; set; }
    }
}
