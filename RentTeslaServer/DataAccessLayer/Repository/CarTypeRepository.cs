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
    public class CarTypeRepository : ICarTypeRepository
    {
        private readonly RentTeslaDbContext dbContext;

        public CarTypeRepository(RentTeslaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<CarType>> GetAllCarTypes()
        {
            return await dbContext.CarTypes.ToListAsync();
        }

        public async Task<CarType?> GetCarTypeById(int id)
        {
            return await dbContext.CarTypes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<CarType?> GetCarTypeByName(string name)
        {
            return await dbContext.CarTypes.FirstOrDefaultAsync(x => x.Name == name);
        }
    }

}
