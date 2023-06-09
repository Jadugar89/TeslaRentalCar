﻿namespace RentTeslaServer.DataAccessLayer.Entities
{
    public class Reservation
    {
        public int Id { get; set; } 
        public Guid Guid { get; set; }

        public int PickUpLocationId { get; set; }
        public CarRental PickUpLocation { get; set; } = null!;
        public DateTime PickUpDate { get; set;}

        public int ReturnLocationId { get; set; }
        public CarRental ReturnLocation { get; set; } = null!;
        public DateTime ReturnDate { get; set; }
        public decimal Cost { get; set; }
        public string Email { get; set; } = null!;

        public int CarId { get; set; }
        public Car Car { get; set; } = null!;

    }
}
