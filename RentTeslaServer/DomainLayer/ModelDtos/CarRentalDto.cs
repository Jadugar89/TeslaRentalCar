namespace DomainLayer.ModelDtos
{
    public class CarRentalDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
    }
}
