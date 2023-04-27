namespace RentTeslaServer.DataAccessLayer.Entities
{
    public class CarRental
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }

        public List<Car> Cars { get; set; }
        public List<Reservation> PickupReservations { get; set; }
        public List<Reservation> DropoffReservations { get; set; }

    }
}
