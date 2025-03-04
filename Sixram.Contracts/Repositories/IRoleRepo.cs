using Sixram.Entities;

namespace Sixram.Contracts.Repositories
{
    public interface IRoleRepo : IBaseRepo<Roles>
    {
        Task<List<Roles>?> GetAllRolesAsync();
    }
}