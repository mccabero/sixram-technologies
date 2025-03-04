using Sixram.Entities;

namespace Sixram.Contracts.Repositories
{
    public interface IUserRoleRepo : IBaseRepo<UserRoles>
    {
        Task<List<UserRoles>?> GetAllUserRolesAsync();

        Task<List<UserRoles>?> GetAllUserRolesByUserIdAsync(int userId);
    }
}