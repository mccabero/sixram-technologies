using AutoMapper;
using Sixram.Web.Api.Profiles;

namespace Sixram.Web.Api.DI
{
    public static class MappingProfileConfiguration
    {
        public static IMapper GetMappingProfileConfiguration()
        {
            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<RoleMappingProfile>();
                cfg.AddProfile<UserMappingProfile>();
                cfg.AddProfile<UserRoleMappingProfile>();
            });

            var mapper = mapConfig.CreateMapper();

            return mapper;
        }
    }
}
