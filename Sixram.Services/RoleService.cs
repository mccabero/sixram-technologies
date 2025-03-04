using Sixram.Common.Extensions;
using Sixram.Contracts.Repositories;
using Sixram.Contracts.Services;
using Sixram.DTO;
using Sixram.Entities;

namespace Sixram.Services
{
    public class RoleService(IRoleRepo repo) : BaseService<Roles, RolesDto>(repo), IRoleService
    {
        public async Task<List<RolesDto>?> GetAllRolesAsync()
        {
            try
            {
                List<RolesDto> dtoList = new List<RolesDto>();

                var entity = await repo.GetAllRolesAsync();

                if (entity == null)
                    return null;

                foreach (var e in entity)
                    dtoList.Add(e.Map<RolesDto>());

                return dtoList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
