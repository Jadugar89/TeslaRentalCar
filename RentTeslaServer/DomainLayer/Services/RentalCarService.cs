using AutoMapper;
using DomainLayer.ModelDtos;
using Microsoft.Extensions.Logging;
using RentTeslaServer.DataAccessLayer.Contracts;
using RentTeslaServer.DomainLayer.Contracts;

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
    }
}
