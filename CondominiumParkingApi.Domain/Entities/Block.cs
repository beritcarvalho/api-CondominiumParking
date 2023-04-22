namespace CondominiumParkingApi.Domain.Entities
{
    public class Block
    {
        public int Id { get; set; }
        public string Block_Name { get; set; }

        public ICollection<Apartment> Apartments { get; set; }
    }
}
