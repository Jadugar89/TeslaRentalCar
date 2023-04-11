using System.ComponentModel.DataAnnotations;

namespace RentTeslaServer.Domain_Layer.ModelDtos
{
    public class ReservationCreateDto
    {
        [EmailAddress]
        public string Email { get; set; }
        public BookCarDto Car { get; set; }
        public SearchDataDto Reservation { get; set; }
    }
}
