using Sixram.Contracts.Services;
using Sixram.Services;

namespace Sixram.Web.Api.DI
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
                    .AddMemoryCache()
                    .AddTransient<IUserService, UserService>()
                    .AddTransient<IRoleService, RoleService>()
                    .AddTransient<IUserRoleService, UserRoleService>();
        }
    }
}
