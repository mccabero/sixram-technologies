using Microsoft.EntityFrameworkCore;
using Sixram.Contracts.Repositories;
using Sixram.Entities;
using Sixram.Repositories.DatabaseContext;

namespace Sixram.Repositories
{
    public class UserRepo(IDbContextFactory<SixramDbContext> context) : BaseRepo<Users>(context), IUserRepo
    {
        public async Task<List<Users>?> GetAllUsersAsync()
        {
            await using var context = await Factory.CreateDbContextAsync();

            return await context.Set<Users>()
                .Include(x => x.Roles)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Users?> GetUserByEmailAsync(string email)
        {
            await using var context = await Factory.CreateDbContextAsync();

            var data = await context.Set<Users>()
                .Include(x => x.Roles)
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Email == email);

            return data;
        }

        public async Task<Users?> GetUserByIdAsync(int id)
        {
            await using var context = await Factory.CreateDbContextAsync();

            var data = await context.Set<Users>()
                .Include(x => x.Roles)
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);

            return data;
        }
    }
}
