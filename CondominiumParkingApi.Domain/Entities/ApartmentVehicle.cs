namespace CondominiumParkingApi.Domain.Entities
{
    public class ApartmentVehicle
    {
        public decimal Id { get; set; }

        public int ApartmentId { get; set; }
        public Apartment Apartment { get; set; }

        public decimal VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        public bool Active { get; set; }
        public DateTime? Active_Date { get; set; }
        public DateTime? Inactive_Date { get; set; }
    }
}
