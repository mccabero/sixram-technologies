using Sixram.Common.Extensions;
using Sixram.Contracts.Repositories;
using Sixram.Contracts.Services;
using Sixram.DTO;
using Sixram.Entities;
using System.Linq.Expressions;

namespace Sixram.Services
{
    public class BaseService<TEntity, TDto>(IBaseRepo<TEntity> repo) 
        : IBaseService<TEntity, TDto> where TDto 
        : BaseDto where TEntity : BaseEntity
    {
        protected IBaseRepo<TEntity> Repo { get; } = repo;

        public virtual async Task<TDto?> GetByIdAsync(int id)
        {
            try
            {
                var entity = await Repo.GetAsync(x => x.Id == id);

                if (entity == null)
                    return null;

                return entity.Map<TDto>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual async Task<TDto?> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var entity = await Repo.GetAsync(predicate);

                if (entity == null)
                    return null;

                return entity.Map<TDto>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual async Task<TDto> CreateAsync(TDto dto)
        {
            try
            {
                var entity = dto.Map<TEntity>();
                var savedUser = await Repo.CreateAsync(entity);

                return savedUser.Map<TDto>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual async Task<TDto> UpdateAsync(TDto dto)
        {
            try
            {
                var entity = dto.Map<TEntity>();
                var data = await Repo.UpdateAsync(entity!);

                return data.Map<TDto>();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual Task DeleteAsync(int id)
        {
            try
            {
                return Repo.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}