using CondominiumParkingApi.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CondominiumParkingApi.Infrastructure.IoC.DependencyInjections
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            #region DataBaseConnection

            services.AddDbContext<DataBaseContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            #endregion

            AddServices(services);
            AddRepository(services);

            return services;
        }

        private static void AddServices(IServiceCollection services)
        {
        }

        private static void AddRepository(IServiceCollection services)
        {
        }
    }
}