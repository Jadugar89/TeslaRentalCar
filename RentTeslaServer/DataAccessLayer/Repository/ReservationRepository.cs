using RentTeslaServer.DataAccessLayer.Contracts;
using RentTeslaServer.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentTeslaServer.DataAccessLayer.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly RentTeslaDbContext dbContext;

        public ReservationRepository(RentTeslaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateReservation(Reservation reservation)
        {
            await dbContext.Reservations.AddAsync(reservation);
            await dbContext.SaveChangesAsync();
        }
    }
}
