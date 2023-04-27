namespace DomainLayer.ModelDtos
{

    public class CarManagmentCreatedDto
    {
        public bool IsPrepared { get; set; }
        public bool IsFree { get; set; }
        public decimal DailyPrice { get; set; }
        public string Plates { get; set; }
        public CarTypeDto CarTypeDto { get; set; }
        public CarRentalDto CarRentalDto { get; set; }
    }
}
