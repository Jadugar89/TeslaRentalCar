namespace RentTeslaServer.DomainLayer.ModelDtos
{
    public class CarManagmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Plates { get; set; } = null!;
        public string CarRentalName { get; set; } = null!;
        public string CarRentalCity { get; set; } = null!;

    }
}
