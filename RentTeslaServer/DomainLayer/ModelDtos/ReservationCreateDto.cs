using System.ComponentModel.DataAnnotations;

namespace DomainLayer.ModelDtos
{
    public class ReservationCreateDto
    {
        [EmailAddress]
        public string Email { get; set; }
        public BookCarDto Car { get; set; }
        public SearchDataDto Reservation { get; set; }
    }
}
