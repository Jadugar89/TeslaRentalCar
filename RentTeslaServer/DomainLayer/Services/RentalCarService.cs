using AutoMapper;
using Microsoft.Extensions.Logging;
using RentTeslaServer.DomainLayer.ModelDtos;
using RentTeslaServer.DataAccessLayer.Contracts;
using RentTeslaServer.DataAccessLayer.Entities;
using RentTeslaServer.DomainLayer.Contracts;
using RentTeslaServer.Exceptions;

namespace DomainLayer.Services
{
    public class RentalCarService : IRentalCarService
    {
        private readonly ILogger<RentalCarService> _logger;
        private readonly IMapper _mapper;
        private readonly ICarRentalRepository _carRentalRepository;

        public RentalCarService(ILogger<RentalCarService> logger, IMapper mapper, ICarRentalRepository carRentalRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _carRentalRepository = carRentalRepository;
        }

        public async Task<IEnumerable<string>> SearchLocalization(string name)
        {
            _logger.LogInformation($"Get localizations for " + name);
            return await _carRentalRepository.GetLocalizationNameAsync(name);
        }

        public async Task<IEnumerable<CarRentalDto>> GetAll()
        {
            var carRentals = await _carRentalRepository.GetAllCarRentalsAsync();
            var carRentalsDto = _mapper.Map<IEnumerable<CarRentalDto>>(carRentals);
            return carRentalsDto;
        }

        public async Task<CarRentalDto> GetCarRentalDtoByIdAsync(int id)
        {
            var carRental = await _carRentalRepository.GetCarRentalByIdAsync(id) ?? throw new NotFoundException("RentalCar with this id not exist");
            return _mapper.Map<CarRentalDto>(carRental);
        }

        public async Task Created(CreatedCarRentalDto createdCarRentalDto)
        {
            if (await _carRentalRepository.CheckName(createdCarRentalDto.Name))
            {
                throw new BadRequestException("Name has to be unique");
            }

            var car = _mapper.Map<CarRental>(createdCarRentalDto);
            await _carRentalRepository.AddCarRentalAsync(car);
        }

        public void Update(int id, CarRentalDto CarRentalDto)
        {
            if (id != CarRentalDto.Id) throw new BadRequestException("Wrong Id");
            var carRental = _mapper.Map<CarRental>(CarRentalDto);
            _carRentalRepository.UpdateCarRental(carRental);
        }

        public async Task Delete(int id)
        {
            _logger.LogInformation($"Car with id: {id} DELETE action invoked");

            var carRental = await _carRentalRepository.GetCarRentalByIdAsync(id) ?? throw new NotFoundException("Car not found");
            _carRentalRepository.DeleteCarRental(carRental);
        }

    }
}
