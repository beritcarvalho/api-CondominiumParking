namespace CondominiumParkingApi.Domain.Entities
{
    public class ParkingSpace
    {
        public int Id { get; set; }
        public int Space { get; set; }
        public bool Handicap { get; set; }

        public Parked Parked { get; set; }
    }
}