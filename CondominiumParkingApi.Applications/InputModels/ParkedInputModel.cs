using CondominiumParkingApi.Domain.Entities;

namespace CondominiumParkingApi.Applications.InputModels
{
    public class ParkedInputModel
    {  
        public int ParkingSpaceId { get; set; }
        public decimal VehicleId { get; set; }
    }
}
