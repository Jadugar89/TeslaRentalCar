using Microsoft.EntityFrameworkCore;
using RentTeslaServer.DataAccessLayer.Contracts;
using RentTeslaServer.DataAccessLayer.Entities;

namespace RentTeslaServer.DataAccessLayer.Repository
{
    public class CarRentalRepository : ICarRentalRepository
    {
        private readonly RentTeslaDbContext _dbContext;

        public CarRentalRepository(RentTeslaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<string>> GetLocalizationNameAsync(string name)
        {
            return await _dbContext.CarRentals.Where(x => x.Name.Contains(name))
                                   .Select(x => x.Name)
                                   .AsNoTracking()
                                   .ToListAsync();
        }

        public async Task<List<CarRental>> GetAllCarRentalsAsync()
        {
            return await _dbContext.CarRentals
                           .AsNoTracking()
                           .ToListAsync();
        }

        public async Task<CarRental?> GetCarRentalByNameAsync(string name)
        {
           return await _dbContext.CarRentals.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<CarRental?> GetCarRentalByIdAsync(int id)
        {
            return await _dbContext.CarRentals.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
