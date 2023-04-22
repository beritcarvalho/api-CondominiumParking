namespace CondominiumParkingApi.Domain.Entities
{
    public class Parked
    {
        public decimal Id { get; set; }
        public int ParkingSpaceId { get; set; }
        public decimal ApartmentVehicleId { get; set; }
        public DateTime In_Date { get; set; }
        public DateTime? Out_Date { get; set; }
        public bool Active { get; set; }
    }
}