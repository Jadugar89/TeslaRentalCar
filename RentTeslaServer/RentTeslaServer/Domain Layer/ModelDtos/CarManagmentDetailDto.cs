using Microsoft.Identity.Client;
using RentTeslaServer.DataAccessLayer.Entities;

namespace RentTeslaServer.Domain_Layer.ModelDtos
{
    public class CarManagmentDetailDto
    {
        public int Id { get; set; }
        public bool IsPrepared { get; set; }
        public bool IsFree { get; set; }
        public double DailyPrice { get; set; }
        public string Plates { get; set; }
        public CarTypeDto carTypeDto { get; set; }
        public CarRentalDto CarRentalDto { get; set; }
    }
}
