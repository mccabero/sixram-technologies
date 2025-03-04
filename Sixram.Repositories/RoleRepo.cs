using Microsoft.EntityFrameworkCore;
using Sixram.Contracts.Repositories;
using Sixram.Entities;
using Sixram.Repositories.DatabaseContext;

namespace Sixram.Repositories
{
    public class RoleRepo(IDbContextFactory<SixramDbContext> context) : BaseRepo<Roles>(context), IRoleRepo
    {
        public async Task<List<Roles>?> GetAllRolesAsync()
        {
            await using var context = await Factory.CreateDbContextAsync();

            return await context
                .Set<Roles>()
                .AsNoTracking()
                .ToListAsync();
        }
    }
}