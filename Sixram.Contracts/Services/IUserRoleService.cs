using Sixram.DTO;
using Sixram.Entities;

namespace Sixram.Contracts.Services
{
    public interface IUserRoleService : IBaseService<UserRoles, UserRolesDto>
    {
        Task<List<UserRolesDto>?> GetAllUserRolesAsync();

        Task<List<UserRolesDto>?> GetAllUserRolesByUserIdAsync(int userId);
    }
}