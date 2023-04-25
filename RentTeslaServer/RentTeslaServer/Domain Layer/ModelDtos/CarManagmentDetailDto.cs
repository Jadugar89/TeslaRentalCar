using Microsoft.Identity.Client;
using RentTeslaServer.DataAccessLayer.Entities;
using RentTeslaServer.Domain_Layer.Validators;

namespace RentTeslaServer.Domain_Layer.ModelDtos
{

    public class CarManagmentDetailDto
    {
        public int Id { get; set; }
        public bool IsPrepared { get; set; }
        public bool IsFree { get; set; }
        public decimal DailyPrice { get; set; }
        public string Plates { get; set; }
        public CarTypeDto CarTypeDto { get; set; }
        public CarRentalDto CarRentalDto { get; set; }
    }
}
