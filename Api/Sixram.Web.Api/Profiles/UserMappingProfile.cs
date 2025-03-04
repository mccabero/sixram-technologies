using Sixram.DTO;
using Sixram.Models.Request;
using Sixram.Models.Response;

namespace Sixram.Web.Api.Profiles
{
    public class UserMappingProfile : AutoMapper.Profile
    {
        public UserMappingProfile()
        {
            // Get
            CreateMap<UsersDto, UserResponseModel>()
                .ForMember(m => m.Roles, o => o.MapFrom(s => s.Roles));

            // Create and Update
            CreateMap<UserRequestModel, UsersDto>();
        }
    }
}
