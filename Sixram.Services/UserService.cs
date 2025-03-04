using AutoMapper;
using Sixram.Common.Extensions;
using Sixram.Contracts.Repositories;
using Sixram.Contracts.Services;
using Sixram.DTO;
using Sixram.Entities;

namespace Sixram.Services
{
    public class UserService(IUserRepo repo) : BaseService<Users, UsersDto>(repo), IUserService
    {
        private static IMapper InitializeMapper()
        {
            var map = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Users, UsersDto>();
                cfg.CreateMap<Roles, RolesDto>();

                cfg.CreateMap<UsersDto, Users>();
                cfg.CreateMap<RolesDto, Roles>();
            });
            var mapper = map.CreateMapper();
            return mapper;
        }

        public async Task<List<UsersDto>?> GetAllUsersAsync()
        {
            try
            {
                List<UsersDto> dtoList = new List<UsersDto>();
                var entityList = await repo.GetAllUsersAsync();

                if (entityList != null && !entityList.Any())
                    return null;

                IMapper mapper = InitializeMapper();

                foreach (var e in entityList!)
                    dtoList.Add(mapper.Map<UsersDto>(e));

                return dtoList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<UsersDto?> GetUserByEmailAsync(string email)
        {
            try
            {
                var entity = await repo.GetUserByEmailAsync(email);
                
                if (entity == null)
                    return null;

                IMapper mapper = InitializeMapper();

                return mapper.Map<UsersDto>(entity);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<UsersDto?> GetUserByIdAsync(int id)
        {
            try
            {
                var entity = await repo.GetUserByIdAsync(id);

                if (entity == null)
                    return null;

                IMapper mapper = InitializeMapper();

                return mapper.Map<UsersDto>(entity);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
