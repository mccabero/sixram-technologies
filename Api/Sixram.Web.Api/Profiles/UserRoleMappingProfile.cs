using Sixram.DTO;
using Sixram.Models.Request;
using Sixram.Models.Response;

namespace Sixram.Web.Api.Profiles
{
    public class UserRoleMappingProfile : AutoMapper.Profile
    {
        public UserRoleMappingProfile()
        {
            // Get
            CreateMap<UserRolesDto, UserRoleResponseModel>()
                .ForMember(m => m.Users, o => o.MapFrom(s => s.Users))
                .ForMember(m => m.Roles, o => o.MapFrom(s => s.Roles));

            // Create and Update
            CreateMap<UserRoleRequestModel, UserRolesDto>()
                .ForMember(m => m.UserId, o => o.MapFrom(s => s.UserId))
                .ForMember(m => m.RoleId, o => o.MapFrom(s => s.RoleId));
        }
    }
}
