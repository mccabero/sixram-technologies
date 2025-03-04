using Microsoft.EntityFrameworkCore;
using Sixram.Entities;

namespace Sixram.Repositories.DatabaseContext
{
    public class SixramDbContext(DbContextOptions<SixramDbContext> options) : DbContext(options)
    {
        // All entities should be in PLURAL form.
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<UserRoles> UserRoles { get; set; }
    }
}