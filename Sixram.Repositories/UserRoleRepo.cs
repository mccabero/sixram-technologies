using Microsoft.EntityFrameworkCore;
using Sixram.Contracts.Repositories;
using Sixram.Entities;
using Sixram.Repositories.DatabaseContext;

namespace Sixram.Repositories
{
    public class UserRoleRepo(IDbContextFactory<SixramDbContext> context) : BaseRepo<UserRoles>(context), IUserRoleRepo
    {
        public async Task<List<UserRoles>?> GetAllUserRolesAsync()
        {
            await using var context = await Factory.CreateDbContextAsync();

            return await context
                .Set<UserRoles>()
                    .Include(x => x.Users)
                    .Include(x => x.Roles)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<UserRoles>?> GetAllUserRolesByUserIdAsync(int userId)
        {
            await using var context = await Factory.CreateDbContextAsync();

            return await context
                .Set<UserRoles>()
                    .Include(x => x.Users)
                    .Include(x => x.Roles)
                .Where(x => x.UserId == userId)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}