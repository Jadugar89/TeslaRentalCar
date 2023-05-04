namespace RentTeslaServer.DomainLayer.ModelDtos
{
    public class CarTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Motor { get; set; } = null!;
        public int Range { get; set; }
        public int Seats { get; set; }

    }
}
