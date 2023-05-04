namespace RentTeslaServer.DomainLayer.ModelDtos
{
    public class CreatedCarRentalDto
    {
            public string Name { get; set; } = null!;
            public string City { get; set; } = null!;
            public string Street { get; set; } = null!;
            public string PostalCode { get; set; } = null!;
    }
}
