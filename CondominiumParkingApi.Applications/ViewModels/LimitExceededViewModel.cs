using CondominiumParkingApi.Domain.Entities;

namespace CondominiumParkingApi.Applications.ViewModels
{
    public class LimitExceededViewModel
    {
        public int Id { get; set; }

        public decimal ParkedId { get; set; }
        public Parked Parked { get; set; }

        public DateTime Deadline { get; set; }
        public TimeSpan Time_Exceeded { get; set; }
    }
}
