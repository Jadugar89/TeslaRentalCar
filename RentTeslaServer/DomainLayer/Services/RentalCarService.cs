using AutoMapper;
using DomainLayer.ModelDtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RentTeslaServer.DataAccessLayer;
using RentTeslaServer.DataAccessLayer.Contracts;
using RentTeslaServer.DomainLayer.Contracts;

namespace DomainLayer.Services
{
    public class RentalCarService : IRentalCarService
    {
        private readonly ILogger<RentalCarService> logger;
        private readonly IMapper mapper;
        private readonly ICarRentalRepository carRentalRepository;

        public RentalCarService(ILogger<RentalCarService> logger, IMapper mapper, ICarRentalRepository carRentalRepository)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.carRentalRepository = carRentalRepository;
        }

        public async Task<IEnumerable<string>> SearchLocalization(string name)
        {
            return await carRentalRepository.GetLocalizationNameAsync(name);
        }

        public async Task<IEnumerable<CarRentalDto>> GetAll()
        {
            var carRentals = await carRentalRepository.GetAllCarRentalsAsync();
            var carRentalsDto = mapper.Map<IEnumerable<CarRentalDto>>(carRentals);
            return carRentalsDto;
        }
    }
}
