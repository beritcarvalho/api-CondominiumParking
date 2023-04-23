namespace CondominiumParkingApi.Domain.Entities
{
    public class ParkingSpace
    {
        public int Id { get; set; }
        public int Space { get; set; }
        public bool Handicap { get; private set; }
        public bool Active { get; private set; }

        public ICollection<Parked> Parkeds { get; set; }

        public void EnableParkingSpace()
        {
            Active = true;
        }

        public void DisableParkingSpace()
        {
            Active = false;
        }

        public void EnableHandicap()
        {
            Active = true;
        }

        public void DisableHandicap()
        {
            Active = false;
        }
    }
}