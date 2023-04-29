using RentTeslaServer.DataAccessLayer.Contracts;
using RentTeslaServer.DataAccessLayer.Entities;

namespace RentTeslaServer.DataAccessLayer.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly RentTeslaDbContext _dbContext;

        public ReservationRepository(RentTeslaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateReservation(Reservation reservation)
        {
            await _dbContext.Reservations.AddAsync(reservation);
            await _dbContext.SaveChangesAsync();
        }
    }
}
