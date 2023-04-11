using RentTeslaServer.Domain_Layer.ModelDtos;

namespace RentTeslaServer.Services
{
    public interface IReservationService
    {
        Task<Guid> CreateReservation(ReservationCreateDto reservationCreateDto);
        void GetReservation();
    }
}