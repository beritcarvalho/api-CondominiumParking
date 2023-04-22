namespace CondominiumParkingApi.Domain.Entities
{
    public class Brand
    {
        public int Id { get; set; }
        public string Brand_Name { get; set; }

        public ICollection<VehicleModel> Models { get; set; }
    }
}
