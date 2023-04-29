namespace DomainLayer.ModelDtos
{
    public class CarDto
    {
        public int Id { get; set; }
        public decimal DailyPrice { get; set; }
        public string Name { get; set; } = null!;
        public string Motor { get; set; } = null!;
        public int Range { get; set; }
        public int Seats { get; set; }

    }
}
