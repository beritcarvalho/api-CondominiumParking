namespace CondominiumParkingApi.Domain.Entities
{
    public class LimitExceeded
    {
        public int Id { get; set; }
        public int ParkingId { get; set; }
        public TimeSpan Limit_Exceeded { get; set; }
    }
}