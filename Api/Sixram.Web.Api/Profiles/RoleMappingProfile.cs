using Sixram.DTO;
using Sixram.Models.Request;
using Sixram.Models.Response;

namespace Sixram.Web.Api.Profiles
{
    public class RoleMappingProfile : AutoMapper.Profile
    {
        public RoleMappingProfile() 
        {
            // Get
            CreateMap<RolesDto, RoleResponseModel>()
                .ForMember(m => m.Id, o => o.MapFrom(s => s.Id))
                .ForMember(m => m.Name, o => o.MapFrom(s => s.Name))
                .ForMember(m => m.Description, o => o.MapFrom(s => s.Description));

            // Create and Update
            CreateMap<RoleRequestModel, RolesDto>()
                .ForMember(m => m.Name, o => o.MapFrom(s => s.Name))
                .ForMember(m => m.Description, o => o.MapFrom(s => s.Description));
        }
    }
}
