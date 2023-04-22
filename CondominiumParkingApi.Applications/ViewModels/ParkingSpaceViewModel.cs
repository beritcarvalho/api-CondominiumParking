using CondominiumParkingApi.Domain.Entities;

namespace CondominiumParkingApi.Applications.ViewModels
{
    public class ParkingSpaceViewModel
    {
        public int Id { get; set; }
        public int Space { get; set; }
        public bool Handicap { get; set; }

        public ICollection<Parked> Parkeds { get; set; }
    }
}
