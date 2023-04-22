namespace CondominiumParkingApi.Domain.Entities
{
    public class ParkingSpace
    {
        public int Id { get; set; }
        public int Space { get; set; }
        public bool Handicap { get; set; }

        public ICollection<Parked> Parkeds { get; set; }
    }
}