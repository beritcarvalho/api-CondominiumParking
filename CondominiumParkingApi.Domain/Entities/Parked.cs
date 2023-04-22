namespace CondominiumParkingApi.Domain.Entities
{
    public class Parked
    {
        public decimal Id { get; set; }

        public int ParkingSpaceId { get; set; }
        public ParkingSpace ParkingSpace { get; set; }

        public decimal ApartmentVehicleId { get; set; }
        public ApartmentVehicle ApartmentVehicle { get; set; }

        public DateTime In_Date { get; private set; }
        public DateTime? Out_Date { get; private set; }

        
        private DateTime deadline;
        public DateTime Deadline
        {
            get { return deadline; }
            private set { deadline = In_Date.AddHours(48); }
        }

        public TimeSpan? Time_Exceeded { get; private set; }
        public bool Active { get; private set; }    


        public void Park()
        {
            In_Date = DateTime.Now;
            Active = true;
        }

        public void Unpark()
        {
            Out_Date = DateTime.Now;
            Active = false;

            if (Deadline > DateTime.Now)
            {
                Time_Exceeded = DateTime.Now - deadline;
            }
        }
    }
}