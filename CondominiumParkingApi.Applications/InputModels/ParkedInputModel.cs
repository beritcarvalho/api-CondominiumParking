using CondominiumParkingApi.Domain.Entities;

namespace CondominiumParkingApi.Applications.InputModels
{
    public class ParkedInputModel
    {
        public decimal ParkedId { get; set; }
        public int ParkingSpaceId { get; set; }
        public decimal ApartmentVehicleId { get; set; }
    }
}
