using CondominiumParkingApi.Domain.Entities;

namespace CondominiumParkingApi.Applications.ViewModels
{
    public class ParkingSpaceViewModel
    {
        public int Id { get; set; }
        public int Space { get; set; }
        public bool Handicap { get; set; }
        public bool Free { get; set; }
        public string Plate { get; set; }
        public string Apartment { get; set; }
        public DateTime? In_Date { get; set; }
        public DateTime? Deadline { get; set; }
    }
}
