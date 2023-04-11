using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RentTeslaServer.DataAccessLayer;
using RentTeslaServer.DataAccessLayer.Entities;

namespace RentTeslaServer.Domain_Layer.Services
{
    public class PurgeReservationService : IPurgeReservationService
    {
        private readonly ILogger<PurgeReservationService> logger;
        private readonly IMapper mapper;
        private readonly RentTeslaDbContext dbContext;

        public PurgeReservationService(ILogger<PurgeReservationService> logger,IMapper mapper, RentTeslaDbContext dbContext)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.dbContext = dbContext;
        }

        public async Task MoveToHistory()
        {
            var threshold = DateTime.Now;
            var recordsToDelete = await dbContext.Reservations.Where(x => x.ReturnDate < threshold).ToListAsync();
            var recordsAddedToHistory= mapper.Map<IEnumerable<History>>(recordsToDelete);

            await dbContext.History.AddRangeAsync(recordsAddedToHistory);
            dbContext.Reservations.RemoveRange(recordsToDelete);
            await dbContext.SaveChangesAsync();
        }/*
        private async CheckWhereIsCar(List<Reservation> reservations)
        {
            var threshold = DateTime.Now;
            

        }*/
    }
}
