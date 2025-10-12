
using System;
using System.ComponentModel.DataAnnotations;

namespace Shared.Data.Entities
{
    public class User
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; } // REQUIRED.

        public string Email { get; set; } = string.Empty;
        public DateTime Birthdate { get; set; }

        [Required]
        [MaxLength(20)]
        public string DisplayName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime Created { get; set; }

        public bool IsBanned { get; set; }

        public bool HasVerifiedBadge { get; set; }
    }
}
