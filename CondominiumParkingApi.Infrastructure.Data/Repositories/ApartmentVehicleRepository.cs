using CondominiumParkingApi.Domain.Entities;
using CondominiumParkingApi.Domain.Interfaces;
using CondominiumParkingApi.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CondominiumParkingApi.Infrastructure.Data.Repositories
{
    public class ApartmentVehicleRepository : BaseRepository<ApartmentVehicle>, IApartmentVehicleRepository
    {
        public ApartmentVehicleRepository(DataBaseContext context) : base(context)
        {
        }

        public async Task<List<ApartmentVehicle>> GetAllApartmentsVehiclesWithInclusions()
        {
            return await Context.ApartmentsVehicles
                .Include(apartVehi => apartVehi.Apartment.Resident)
                .Include(apartVehi => apartVehi.Apartment.Block)
                .Include(apartVehi => apartVehi.Vehicle.VehicleModel.Brand)
                .ToListAsync();
        }

        public async Task<ApartmentVehicle> GetApartmentVehicleWithInclusions(decimal idVehicle, int idApartment)
        {
            return await Context.ApartmentsVehicles
                .Where(apartVehi => apartVehi.VehicleId == idVehicle && apartVehi.ApartmentId == idApartment)
                .Include(apartVehi => apartVehi.Apartment.Resident)
                .Include(apartVehi => apartVehi.Apartment.Block)
                .Include(apartVehi => apartVehi.Vehicle.VehicleModel.Brand)
                .FirstOrDefaultAsync();
        }

        public async Task<ApartmentVehicle> GetActiveLinkByVehicleIdWithInclusions(decimal vehicleId)
        {
            return await Context.ApartmentsVehicles
                .Where(apartVehi => apartVehi.VehicleId == vehicleId && apartVehi.Active)
                .Include(apartVehi => apartVehi.Apartment.Resident)
                .Include(apartVehi => apartVehi.Apartment.Block)
                .Include(apartVehi => apartVehi.Vehicle.VehicleModel.Brand)
                .FirstOrDefaultAsync();
        }

        public async Task<ApartmentVehicle> GetActiveLinkById(int id)
        {
            var apartmentVehicle = await Context
                .ApartmentsVehicles
                .Where(apartVehi => apartVehi.ApartmentId == id && apartVehi.Active)
                .FirstOrDefaultAsync();

            return apartmentVehicle;
        }
    }
}
