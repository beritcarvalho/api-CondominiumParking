using CondominiumApi.Infrastructure.Data.Configurations.EntityConfigurations;
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new PersonConfiguration());
            builder.ApplyConfiguration(new ApartmentConfiguration());
            builder.ApplyConfiguration(new BlockConfiguration());
            builder.ApplyConfiguration(new VehicleConfiguration());
            builder.ApplyConfiguration(new VehicleModelConfiguration());
            builder.ApplyConfiguration(new BrandConfiguration());
            builder.ApplyConfiguration(new ApartmentVehicleConfiguration());
            builder.ApplyConfiguration(new ParkedConfiguration());
            builder.ApplyConfiguration(new ParkingSpaceConfiguration());
            builder.ApplyConfiguration(new LimitExceededConfiguration());
        }
    }
}
