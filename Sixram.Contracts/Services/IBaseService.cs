using Sixram.DTO;
using Sixram.Entities;
using System.Linq.Expressions;

namespace Sixram.Contracts.Services
{
    public interface IBaseService<TEntity, TDto>
                where TEntity : BaseEntity
                where TDto : BaseDto
    {
        Task<TDto> CreateAsync(TDto dto);

        Task DeleteAsync(int id);

        Task<TDto?> GetAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TDto?> GetByIdAsync(int id);

        Task<TDto> UpdateAsync(TDto dto);
    }
}