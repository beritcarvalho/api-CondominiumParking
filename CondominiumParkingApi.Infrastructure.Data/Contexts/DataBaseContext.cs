using CondominiumParkingApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CondominiumParkingApi.Infrastructure.Data.Contexts
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext() 
        {
        }

        public DataBaseContext(DbContextOptions<DataBaseContext> options) 
            : base(options)
        {
        }

        public DbSet<Person> People { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Block> Blocks { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleModel> VehiclesModels { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ApartmentVehicle> ApartmentsVehicles { get; set; }
        public DbSet<Parked> Parkeds { get; set; }
        public DbSet<ParkingSpace> ParkingSpaces { get; set; }
        public DbSet<LimitExceeded> LimitsExceeded { get; set; }
    }
}
