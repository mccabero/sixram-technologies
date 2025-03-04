using Sixram.Entities;

namespace Sixram.Contracts.Repositories
{
    public interface IUserRepo : IBaseRepo<Users>
    {
        Task<Users?> GetUserByIdAsync(int id);

        Task<Users?> GetUserByEmailAsync(string email);

        Task<List<Users>?> GetAllUsersAsync();
    }
}
