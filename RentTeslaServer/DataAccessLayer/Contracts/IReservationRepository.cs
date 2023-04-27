using RentTeslaServer.DataAccessLayer.Entities;

namespace RentTeslaServer.DataAccessLayer.Contracts
{
    public interface IReservationRepository
    {
        Task CreateReservation(Reservation reservation);
    }
}