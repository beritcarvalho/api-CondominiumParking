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


        public DateTime Deadline { get; private set; }

        public double? Total_Exceeded_Minutes { get; private set; }
        public bool Active { get; private set; }    


        public void Park()
        {
            In_Date = DateTime.Now;
            Deadline = In_Date.AddHours(48);
            Active = true;
        }

        public void Unpark()
        {
            Out_Date = DateTime.Now;
            Active = false;

            if (DateTime.Now > Deadline)
            {                
                Total_Exceeded_Minutes = (DateTime.Now - Deadline).TotalMinutes;
            }
        }
    }
}