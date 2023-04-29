using AutoMapper;
using DomainLayer.ModelDtos;
using Microsoft.Extensions.Logging;
using RentTeslaServer.DataAccessLayer.Contracts;
using RentTeslaServer.DomainLayer.Contracts;
using RentTeslaServer.Exceptions;

namespace DomainLayer.Services
{
    public class CarTypeService : ICarTypeService
    {

        private readonly ILogger<CarTypeService> _logger;
        private readonly IMapper _mapper;
        private readonly ICarTypeRepository _carTypeRepository;

        public CarTypeService(ILogger<CarTypeService> logger,IMapper mapper, ICarTypeRepository carTypeRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _carTypeRepository = carTypeRepository;
        }

        public async Task<IEnumerable<CarTypeDto>> GetAll()
        {
            _logger.LogInformation("Get data for Cars");
            var carTypes = await _carTypeRepository.GetAllCarTypes();
            var carTypesDto = _mapper.Map<IEnumerable<CarTypeDto>>(carTypes);
            return carTypesDto;
        }

        public async Task<CarTypeDto> GetById(int id)
        {
            _logger.LogInformation($"Get data for car id={id}");
            var carType = await _carTypeRepository.GetCarTypeById(id) ?? throw new NotFoundException("Car type with this id not exist");
            var carTypeDto = _mapper.Map<CarTypeDto>(carType);
            return carTypeDto;
        }

        public async Task<CarTypeDto> GetByName(string name)
        {
            _logger.LogInformation($"Get data for car name={name}");
            var carType = await _carTypeRepository.GetCarTypeByName(name) ?? throw new NotFoundException("Car type with this name not exist");
            var carTypeDto = _mapper.Map<CarTypeDto>(carType);
            return carTypeDto;
        }

    }
}
