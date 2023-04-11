using Microsoft.EntityFrameworkCore;
using RentTeslaServer.DataAccessLayer;
using RentTeslaServer.Domain_Layer.ModelDtos;

namespace RentTeslaServer.Services
{
    public class RentalCarService : IRentalCarService
    {
        private readonly ILogger<RentalCarService> logger;
        private readonly RentTeslaDbContext dbContext;

        public RentalCarService(ILogger<RentalCarService> logger, RentTeslaDbContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<string>> SearchLocalization(string name)
        {

             return await dbContext.CarRentals.Where(x => x.Name.Contains(name)).Select(x => x.Name).ToListAsync();
        
        }
    }
}
