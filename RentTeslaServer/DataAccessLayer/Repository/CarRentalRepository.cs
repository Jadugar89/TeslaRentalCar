using Microsoft.EntityFrameworkCore;
using RentTeslaServer.DataAccessLayer.Contracts;
using RentTeslaServer.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentTeslaServer.DataAccessLayer.Repository
{
    public class CarRentalRepository : ICarRentalRepository
    {
        private readonly RentTeslaDbContext dbContext;

        public CarRentalRepository(RentTeslaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<string>> GetLocalizationNameAsync(string name)
        {
            return await dbContext.CarRentals.Where(x => x.Name.Contains(name))
                                   .Select(x => x.Name)
                                   .AsNoTracking()
                                   .ToListAsync();
        }

        public async Task<List<CarRental>> GetAllCarRentalsAsync()
        {
            return await dbContext.CarRentals
                           .AsNoTracking()
                           .ToListAsync();
        }

        public async Task<CarRental?> GetCarRentalByNameAsync(string name)
        {
           return await dbContext.CarRentals.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<CarRental?> GetCarRentalByIdAsync(int id)
        {
            return await dbContext.CarRentals.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
