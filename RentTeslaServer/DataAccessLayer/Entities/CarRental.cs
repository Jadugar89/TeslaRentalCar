namespace RentTeslaServer.DataAccessLayer.Entities
{
    public class CarRental
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsActive { get; set; }
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string PostalCode { get; set; } = null!;

        public List<Car>? Cars { get; set; }
        public List<Reservation>? PickupReservations { get; set; }
        public List<Reservation>? DropoffReservations { get; set; }

    }
}
