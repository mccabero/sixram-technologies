using Microsoft.EntityFrameworkCore;
using Sixram.Contracts.Repositories;
using Sixram.Entities;
using Sixram.Repositories.DatabaseContext;
using System.Linq.Expressions;

namespace Sixram.Repositories
{
    public class BaseRepo<T>(IDbContextFactory<SixramDbContext> context) : IBaseRepo<T> where T : BaseEntity
    {
        protected IDbContextFactory<SixramDbContext> Factory { get; } = context;

        public virtual async Task<T?> GetAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                await using var context = await Factory.CreateDbContextAsync();

                return await context
                    .Set<T>()
                    .AsNoTracking()
                    .SingleOrDefaultAsync(predicate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual async Task<List<T>> GetManyAsync(Expression<Func<T, bool>>? predicate = null, int offset = 0, int limit = 10)
        {
            try
            {
                await using var context = await Factory.CreateDbContextAsync();

                var query = predicate != null
                    ? context.Set<T>().Where(predicate)
                    : context.Set<T>();

                return await query.Take(limit).Skip(offset).ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual async Task<T> CreateAsync(T entity)
        {
            try
            {
                await using var context = await Factory.CreateDbContextAsync();
                await context.Set<T>().AddAsync(entity);
                await context.SaveChangesAsync();

                // detach entity to prevent tracking when converted into DTO
                context.Entry(entity).State = EntityState.Detached;

                return entity;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            try
            {
                await using var context = await Factory.CreateDbContextAsync();

                context.Attach(entity);
                var state = context.Set<T>()
                    .Update(entity).State = EntityState.Modified;

                await context.SaveChangesAsync();

                // detach entity to prevent tracking when converted into DTO
                context.Entry(entity).State = EntityState.Detached;

                return entity;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual async Task DeleteAsync(int id)
        {
            try
            {
                await using var context = await Factory.CreateDbContextAsync();
                await context.Set<T>()
                    .Where(x => x.Id == id)
                    .ExecuteDeleteAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}