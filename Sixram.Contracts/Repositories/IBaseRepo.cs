using Sixram.Entities;
using System.Linq.Expressions;

namespace Sixram.Contracts.Repositories
{
    public interface IBaseRepo<T> where T : BaseEntity
    {
        Task<T> CreateAsync(T entity);

        Task DeleteAsync(int id);

        Task<T?> GetAsync(Expression<Func<T, bool>> predicate);

        Task<List<T>> GetManyAsync(Expression<Func<T, bool>>? predicate = null, int offset = 0, int limit = 10);

        Task<T> UpdateAsync(T entity);
    }
}