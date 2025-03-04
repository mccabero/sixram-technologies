using Sixram.DTO;
using Sixram.Entities;

namespace Sixram.Contracts.Services
{
    public interface IRoleService : IBaseService<Roles, RolesDto>
    {
        Task<List<RolesDto>?> GetAllRolesAsync();
    }
}