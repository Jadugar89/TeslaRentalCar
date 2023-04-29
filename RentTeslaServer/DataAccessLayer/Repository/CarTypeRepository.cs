using Microsoft.EntityFrameworkCore;
using RentTeslaServer.DataAccessLayer.Contracts;
using RentTeslaServer.DataAccessLayer.Entities;

namespace RentTeslaServer.DataAccessLayer.Repository
{
    public class CarTypeRepository : ICarTypeRepository
    {
        private readonly RentTeslaDbContext _dbContext;

        public CarTypeRepository(RentTeslaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<CarType>> GetAllCarTypes()
        {
            return await _dbContext.CarTypes.ToListAsync();
        }

        public async Task<CarType?> GetCarTypeById(int id)
        {
            return await _dbContext.CarTypes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<CarType?> GetCarTypeByName(string name)
        {
            return await _dbContext.CarTypes.FirstOrDefaultAsync(x => x.Name == name);
        }
    }

}
