using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RentTeslaServer.Controllers;
using RentTeslaServer.DataAccessLayer;
using RentTeslaServer.Domain_Layer.ModelDtos;
using RentTeslaServer.Exceptions;

namespace RentTeslaServer.Domain_Layer.Services
{
    public class CarTypeService : ICarTypeService
    {

        private readonly ILogger<CarTypeController> logger;
        private readonly RentTeslaDbContext dbContext;
        private readonly IMapper mapper;

        public CarTypeService(ILogger<CarTypeController> logger, RentTeslaDbContext dbContext, IMapper mapper)
        {
            this.logger = logger;
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<CarTypeDto>> GetAll()
        {
            var carTypes = await dbContext.CarTypes.ToListAsync();
            var carTypesDto = mapper.Map<IEnumerable<CarTypeDto>>(carTypes);
            return carTypesDto;
        }

        public async Task<CarTypeDto> GetById(int id)
        {
            var carType = await dbContext.CarTypes.FirstOrDefaultAsync(x => x.Id == id);
            if (carType == null)
                throw new NotFoundException("Car type with this id not exist");
            var carTypeDto = mapper.Map<CarTypeDto>(carType);
            return carTypeDto;
        }

        public async Task<CarTypeDto> GetByName(string name)
        {
            var carType = await dbContext.CarTypes.FirstOrDefaultAsync(x => x.Name == name);
            if (carType == null)
                throw new NotFoundException("Car type with this name not exist");
            var carTypeDto = mapper.Map<CarTypeDto>(carType);
            return carTypeDto;
        }

    }
}
