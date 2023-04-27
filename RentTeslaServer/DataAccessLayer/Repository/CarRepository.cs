using Microsoft.EntityFrameworkCore;
using RentTeslaServer.DataAccessLayer.Contracts;
using RentTeslaServer.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace RentTeslaServer.DataAccessLayer.Repository
{
    public class CarRepository : ICarRepository
    {
        private readonly RentTeslaDbContext dbContext;

        public CarRepository(RentTeslaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Car>> GetAllCarsAsync()
        {
            return await dbContext.Cars.Include(x => x.CarType)
                 .Include(x => x.CarRental)
                 .AsNoTracking()
                 .ToListAsync();
        }

        public async Task<Car?> GetCarAsyncById(int id)
        {
            return await dbContext.Cars
                        .Include(x => x.CarType)
                        .Include(x => x.CarRental)
                        .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Car>> GetCarsByFunction(string name)
        {
            return await dbContext.Cars.Where(x => x.CarRental.Name == name ||
                                                x.Reservations.Any(c => c.ReturnLocation.Name == name)
                                                && x.IsPrepared)
                                        .Include(x => x.CarType)
                                        .Include(x => x.Reservations)
                                        .AsNoTracking()
                                        .ToListAsync();
        }

        public async Task<bool> CheckPlates(string plates)
        {
            return await dbContext.Cars.AnyAsync(x => x.Plates == plates);
        }

        public async Task AddCarAsync(Car car)
        {
            await dbContext.AddAsync(car);
            await dbContext.SaveChangesAsync();
        }

        public void DeleteCar(Car car)
        {
            dbContext.Cars.Remove(car);
            dbContext.SaveChanges();
        }

        public void UpdateCar(Car car)
        {
            dbContext.Cars.Update(car);
            dbContext.SaveChanges();
        }
    }
}
