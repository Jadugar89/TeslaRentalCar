using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RentTeslaServer.DataAccessLayer;
using RentTeslaServer.DataAccessLayer.Entities;
using RentTeslaServer.DomainLayer.Contracts;

namespace DomainLayer.Services
{
    public class PurgeReservationService : IPurgeReservationService
    {
        private readonly ILogger<PurgeReservationService> _logger;
        private readonly IMapper _mapper;
        private readonly RentTeslaDbContext _dbContext;

        public PurgeReservationService(ILogger<PurgeReservationService> logger, IMapper mapper, RentTeslaDbContext dbContext)
        {
            _logger = logger;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task MoveToHistory()
        {
            _logger.LogInformation("Move to history");
            var threshold = DateTime.Now;
            var recordsToDelete = await _dbContext.Reservations.Where(x => x.ReturnDate < threshold).ToListAsync();
            var recordsAddedToHistory = _mapper.Map<IEnumerable<History>>(recordsToDelete);

            await _dbContext.History.AddRangeAsync(recordsAddedToHistory);
            _dbContext.Reservations.RemoveRange(recordsToDelete);
            await _dbContext.SaveChangesAsync();
        }
    }
}
