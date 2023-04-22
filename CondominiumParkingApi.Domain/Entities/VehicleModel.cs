namespace CondominiumParkingApi.Domain.Entities
{
    public class VehicleModel
    {
        public int Id { get; set; }
        public string Model_Name { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
