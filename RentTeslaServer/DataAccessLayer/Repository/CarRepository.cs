using Microsoft.EntityFrameworkCore;
using RentTeslaServer.DataAccessLayer.Contracts;
using RentTeslaServer.DataAccessLayer.Entities;

namespace RentTeslaServer.DataAccessLayer.Repository
{
    public class CarRepository : ICarRepository
    {
        private readonly RentTeslaDbContext _dbContext;

        public CarRepository(RentTeslaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Car>> GetAllCarsAsync()
        {
            return await _dbContext.Cars.Include(x => x.CarType)
                 .Include(x => x.CarRental)
                 .AsNoTracking()
                 .ToListAsync();
        }

        public async Task<Car?> GetCarAsyncById(int id)
        {
            return await _dbContext.Cars
                        .Include(x => x.CarType)
                        .Include(x => x.CarRental)
                        .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Car>> GetCarsByFunction(string name)
        {
            return await _dbContext.Cars.Where(x => x.CarRental.Name == name ||
                                               (x.Reservations!=null && x.Reservations.Any(c => c.ReturnLocation.Name == name))
                                                && x.IsPrepared)
                                        .Include(x => x.CarType)
                                        .Include(x => x.Reservations)
                                        .AsNoTracking()
                                        .ToListAsync();
        }

        public async Task<bool> CheckPlates(string plates)
        {
            return await _dbContext.Cars.AnyAsync(x => x.Plates == plates);
        }

        public async Task AddCarAsync(Car car)
        {
            await _dbContext.Cars.AddAsync(car);
            await _dbContext.SaveChangesAsync();
        }

        public void DeleteCar(Car car)
        {
            _dbContext.Cars.Remove(car);
            _dbContext.SaveChanges();
        }

        public void UpdateCar(Car car)
        {
            _dbContext.Cars.Update(car);
            _dbContext.SaveChanges();
        }
    }
}
