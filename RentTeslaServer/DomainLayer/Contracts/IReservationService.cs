using RentTeslaServer.DomainLayer.ModelDtos;

namespace RentTeslaServer.DomainLayer.Contracts
{
    public interface IReservationService
    {
        Task<Guid> CreateReservation(ReservationCreateDto reservationCreateDto);
    }
}