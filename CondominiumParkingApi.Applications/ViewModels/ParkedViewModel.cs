using CondominiumParkingApi.Domain.Entities;

namespace CondominiumParkingApi.Applications.ViewModels
{
    public class ParkedViewModel
    {
        public decimal Id { get; set; }
        public int ParkingSpaceId { get; set; }
        public decimal ApartmentVehicleId { get; set; }        
        public DateTime In_Date { get; set; }
        public DateTime? Out_Date { get; set; }
        private DateTime deadline;

        public DateTime Deadline
        {
            get 
            {
                deadline = In_Date.AddHours(48);
                return deadline; 
            }            
        }

        public bool Active { get; set; }

        private double? total_Exceeded_Minutes;
        public double? Total_Exceeded_Minutes
        {
            get
            {
                TimeExceeded(total_Exceeded_Minutes);
                return total_Exceeded_Minutes;
            }
            set
            {
                total_Exceeded_Minutes = value;
            }
        }
        public bool Exceeded { get; private set; }
        public TimeSpan? Time_Exceeded { get; private set; }

        

        private void TimeExceeded(double? minutes)
        {
            Exceeded = minutes.HasValue;
            TimeSpan time = TimeSpan.FromHours(minutes.HasValue ? (double)minutes : 0);
            string formattedTime = time.ToString(@"dd\.hh\:mm\:ss");
            Time_Exceeded = TimeSpan.Parse(formattedTime);
        }
    }
}
