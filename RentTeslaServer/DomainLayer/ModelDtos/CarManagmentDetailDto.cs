namespace RentTeslaServer.DomainLayer.ModelDtos
{

    public class CarManagmentDetailDto
    {
        public int Id { get; set; }
        public bool IsPrepared { get; set; }
        public bool IsFree { get; set; }
        public decimal DailyPrice { get; set; }
        public string Plates { get; set; } = null!;
        public CarTypeDto CarTypeDto { get; set; } = null!;
        public CarRentalDto CarRentalDto { get; set; } = null!;
    }
}
