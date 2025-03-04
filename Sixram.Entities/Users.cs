using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sixram.Entities
{
    public class Users : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string? Email { get; set; }

        [Required]
        [StringLength(255)]
        public string? PasswordHash { get; set; }

        [Required]
        [StringLength(255)]
        public string? Salt { get; set; }

        [Required]
        [ForeignKey("RoleId")]
        public Roles Roles { get; set; }
        public int RoleId { get; set; }

        public int Gender { get; set; }

        [Required]
        [StringLength(100)]
        public string? FirstName { get; set; }

        [StringLength(100)]
        public string? MiddleName { get; set; }

        [Required]
        [StringLength(100)]
        public string? LastName { get; set; }

        [Required]
        [StringLength(100)]
        public string? MobileNumber { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}