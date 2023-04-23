using CondominiumParkingApi.Applications.Interfaces;
using CondominiumParkingApi.Applications.Services;
using CondominiumParkingApi.Domain.Interfaces;
using CondominiumParkingApi.Infrastructure.Data.Contexts;
using CondominiumParkingApi.Infrastructure.Data.Repositories;
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

            AddConfigurations(services);
            AddServices(services);
            AddRepository(services);

            return services;
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CondominiumParkingApi.Applications.Mappings.MappingAssemblyMarker));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());            
            services.AddScoped<IParkedService, ParkedService>();
            services.AddScoped<IParkingSpaceService, ParkingSpaceService>();
        }

        private static void AddRepository(IServiceCollection services)
        {
            services.AddScoped<IParkedRepository, ParkedRepository>();
            services.AddScoped<IParkingSpaceRepository, ParkingSpaceRepository>();
            services.AddScoped<IApartmentVehicleRepository, ApartmentVehicleRepository>();
        }

        private static void AddConfigurations(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CondominiumParkingApi.Applications.Mappings.MappingAssemblyMarker));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}