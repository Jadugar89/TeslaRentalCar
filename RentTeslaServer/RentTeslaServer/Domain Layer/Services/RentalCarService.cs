using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RentTeslaServer.DataAccessLayer;
using RentTeslaServer.Domain_Layer.ModelDtos;

namespace RentTeslaServer.Services
{
    public class RentalCarService : IRentalCarService
    {
        private readonly ILogger<RentalCarService> logger;
        private readonly RentTeslaDbContext dbContext;
        private readonly IMapper mapper;

        public RentalCarService(ILogger<RentalCarService> logger, RentTeslaDbContext dbContext, IMapper mapper)
        {
            this.logger = logger;
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<string>> SearchLocalization(string name)
        {
            return await dbContext.CarRentals.Where(x => x.Name.Contains(name))
               .Select(x => x.Name)
               .AsNoTracking()
               .ToListAsync();
        }

        public async Task<IEnumerable<CarRentalDto>> GetAll()
        {
            var carRentals = await dbContext.CarRentals
                           .AsNoTracking()
                           .ToListAsync();
            var carRentalsDto = mapper.Map<IEnumerable<CarRentalDto>>(carRentals);
            return carRentalsDto;
        }


    }
}
