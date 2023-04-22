namespace CondominiumParkingApi.Domain.Entities
{
    public class LimitExceeded
    {
        public int Id { get; set; }

        public decimal ParkedId { get; set; }
        public Parked Parked { get; set; }

        public TimeSpan Time_Exceeded { get; set; }
    }
}