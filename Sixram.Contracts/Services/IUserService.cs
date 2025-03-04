using Sixram.DTO;
using Sixram.Entities;

namespace Sixram.Contracts.Services
{
    public interface IUserService : IBaseService<Users, UsersDto>
    {
        Task<UsersDto?> GetUserByIdAsync(int id);

        Task<UsersDto?> GetUserByEmailAsync(string email);

        Task<List<UsersDto>?> GetAllUsersAsync();
    }
}
