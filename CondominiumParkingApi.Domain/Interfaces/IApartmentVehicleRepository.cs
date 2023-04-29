using CondominiumParkingApi.Domain.Entities;

namespace CondominiumParkingApi.Domain.Interfaces
{
    public interface IApartmentVehicleRepository : IBaseRepository<ApartmentVehicle>
    {
        Task<List<ApartmentVehicle>> GetAllApartmentsVehiclesWithInclusions();
        Task<ApartmentVehicle> GetApartmentVehicleWithInclusions(decimal vehicleId, int idApartment);
        Task<ApartmentVehicle> GetActiveLinkById(int id);
        Task<ApartmentVehicle> GetActiveLinkByVehicleIdWithInclusions(decimal vehicleId); 
    }
}
