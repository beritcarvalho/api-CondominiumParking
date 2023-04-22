namespace CondominiumParkingApi.Domain.Entities
{

    public class Person
    {
        public Guid Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Cpf { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime Create_Date { get; set; }
        public DateTime? Last_Update_Date { get; set; }

        public Apartment ApartmentOwner { get; set; }
        public Apartment ApartmentResident { get; set; }

    }
}
