namespace RentTeslaServer.Domain_Layer.ModelDtos
{
    public class CarRentalDto
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
    }
}
