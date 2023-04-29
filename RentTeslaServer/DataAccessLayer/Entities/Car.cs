namespace RentTeslaServer.DataAccessLayer.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public bool IsPrepared { get; set; }
        public bool IsFree { get; set; }  
        public decimal DailyPrice { get; set; }
        public string Plates { get; set; } = null!;

        public int CarTypeId { get; set; }
        public CarType CarType { get; set; } = null!;

        public int CarRentalId { get; set; }
        public CarRental CarRental { get; set; } = null!;

        public List<Reservation>? Reservations { get; set; }
    }
}
