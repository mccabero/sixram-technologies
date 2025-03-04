using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Sixram.Entities
{
    public class UserRoles : BaseEntity
    {
        [Required]
        [ForeignKey("UserId")]
        public Users Users { get; set; }
        public int UserId { get; set; }

        [Required]
        [ForeignKey("RoleId")]
        public Roles Roles { get; set; }
        public int RoleId { get; set; }
    }
}
