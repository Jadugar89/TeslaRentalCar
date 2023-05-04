using System.ComponentModel.DataAnnotations;

namespace RentTeslaServer.DomainLayer.ModelDtos
{
    public class ReservationCreateDto
    {
        [EmailAddress]
        public string Email { get; set; } = null!;
        public BookCarDto Car { get; set; } = null!;
        public SearchDataDto Reservation { get; set; } = null!;
    }
}
