namespace RentTeslaServer.DataAccessLayer.Entities
{
    public class History
    {
        public int Id { get; set; } 
        public Guid Guid { get; set; }


        public string PickUpLocation { get; set; }
        public DateTime PickUpDate { get; set;}

        public string ReturnLocation { get; set; }
        public DateTime ReturnDate { get; set; }
        public decimal Cost { get; set; }
        public string Email { get; set; }
       
        public int CarId { get; set; }
        public Car Car { get; set; }
       
    }
}
