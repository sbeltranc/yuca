using System;
using System.ComponentModel.DataAnnotations;

namespace UsersService.Entities
{
    public class User
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [MaxLength(20)]
        public string DisplayName { get; set; }

        public string Description { get; set; }

        public DateTime Created { get; set; }

        public bool IsBanned { get; set; }

        public bool HasVerifiedBadge { get; set; }
    }
}
