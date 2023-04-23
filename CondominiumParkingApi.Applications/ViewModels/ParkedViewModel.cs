using CondominiumParkingApi.Domain.Entities;

namespace CondominiumParkingApi.Applications.ViewModels
{
    public class ParkedViewModel
    {
        public decimal Id { get; set; }
        public int ParkingSpaceId { get; set; }
        public decimal ApartmentVehicleId { get; set; }        
        public DateTime In_Date { get; set; }
        public DateTime? Out_Date { get; set; }
        public DateTime Deadline { get; set; }
        public bool Active { get; set; }
        public bool Exceeded { get; set; }
        public TimeSpan? Time_Exceeded { get; set; }
    }
}
