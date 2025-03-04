using Sixram.Contracts.Repositories;
using Sixram.Repositories;

namespace Sixram.Web.Api.DI
{
    public static class RepositoryConfiguration
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddTransient<IUserRepo, UserRepo>()
                .AddTransient<IRoleRepo, RoleRepo>()
                .AddTransient<IUserRoleRepo, UserRoleRepo>();
        }
    }
}
