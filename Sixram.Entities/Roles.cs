using System.ComponentModel.DataAnnotations;

namespace Sixram.Entities
{
    public class Roles : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }
    }
}
