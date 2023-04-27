using AutoMapper;
using DomainLayer.ModelDtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RentTeslaServer.DataAccessLayer;
using RentTeslaServer.DataAccessLayer.Contracts;
using RentTeslaServer.DomainLayer.Contracts;
using RentTeslaServer.Exceptions;

namespace DomainLayer.Services
{
    public class CarTypeService : ICarTypeService
    {

        private readonly ILogger<CarTypeService> logger;
        private readonly IMapper mapper;
        private readonly ICarTypeRepository carTypeRepository;

        public CarTypeService(ILogger<CarTypeService> logger,IMapper mapper, ICarTypeRepository carTypeRepository)
        {
            this.logger = logger; 
            this.mapper = mapper;
            this.carTypeRepository = carTypeRepository;
        }

        public async Task<IEnumerable<CarTypeDto>> GetAll()
        {
            var carTypes = await carTypeRepository.GetAllCarTypes();
            var carTypesDto = mapper.Map<IEnumerable<CarTypeDto>>(carTypes);
            return carTypesDto;
        }

        public async Task<CarTypeDto> GetById(int id)
        {
            var carType = await carTypeRepository.GetCarTypeById(id);
            if (carType == null)
                throw new NotFoundException("Car type with this id not exist");
            var carTypeDto = mapper.Map<CarTypeDto>(carType);
            return carTypeDto;
        }

        public async Task<CarTypeDto> GetByName(string name)
        {
            var carType = await carTypeRepository.GetCarTypeByName(name);
            if (carType == null)
                throw new NotFoundException("Car type with this name not exist");
            var carTypeDto = mapper.Map<CarTypeDto>(carType);
            return carTypeDto;
        }

    }
}
