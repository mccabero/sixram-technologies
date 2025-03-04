using Microsoft.EntityFrameworkCore;
using Sixram.Repositories.DatabaseContext;

namespace Sixram.Web.Api.DI
{
    public static class SixramDbContextConfiguration
    {
        public static IServiceCollection AddSixramDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            return services
                .AddDbContextFactory<SixramDbContext>(option => option.UseSqlServer(connectionString))
                .AddDbContextFactory<DbContext>(option => {
                    option.UseSqlServer(connectionString);
                    option.EnableSensitiveDataLogging();
                });
        }
    }
}
