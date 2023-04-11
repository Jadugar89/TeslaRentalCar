namespace RentTeslaServer.DataAccessLayer.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public bool IsPrepared { get; set; }
        public bool IsFree { get; set; }  
        public double DailyPrice { get; set; }

        public int CarTypeId { get; set; }
        public CarType CarType { get; set; }

        public int CarRentalId { get; set; }
        public CarRental CarRental { get; set; }

        public List<Reservation> Reservations { get; set; }
    }
}
