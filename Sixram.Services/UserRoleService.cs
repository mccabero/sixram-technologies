using AutoMapper;
using Sixram.Contracts.Repositories;
using Sixram.Contracts.Services;
using Sixram.DTO;
using Sixram.Entities;

namespace Sixram.Services
{
    public class UserRoleService(IUserRoleRepo repo) : BaseService<UserRoles, UserRolesDto>(repo), IUserRoleService
    {
        private static IMapper InitializeMapper()
        {
            var map = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Users, UsersDto>();
                cfg.CreateMap<Roles, RolesDto>();
                cfg.CreateMap<UserRoles, UserRolesDto>();

                cfg.CreateMap<UserRolesDto, UserRoles> ();
                cfg.CreateMap<UsersDto, Users>();
                cfg.CreateMap<RolesDto, Roles>();
            });
            var mapper = map.CreateMapper();
            return mapper;
        }

        public async Task<List<UserRolesDto>?> GetAllUserRolesAsync()
        {
            try
            {
                List<UserRolesDto> dtoList = new List<UserRolesDto>();

                var entity = await repo.GetAllUserRolesAsync();

                if (entity == null)
                    return null;

                IMapper mapper = InitializeMapper();

                foreach (var e in entity)
                    dtoList.Add(mapper.Map<UserRolesDto>(e));

                return dtoList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<UserRolesDto>?> GetAllUserRolesByUserIdAsync(int userId)
        {
            try
            {
                List<UserRolesDto> dtoList = new List<UserRolesDto>();

                var entity = await repo.GetAllUserRolesByUserIdAsync(userId);

                if (entity == null)
                    return null;

                IMapper mapper = InitializeMapper();

                foreach (var e in entity)
                    dtoList.Add(mapper.Map<UserRolesDto>(e));

                return dtoList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
