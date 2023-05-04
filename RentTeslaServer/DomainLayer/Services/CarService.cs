using AutoMapper;
using Microsoft.Extensions.Logging;
using RentTeslaServer.DataAccessLayer.Contracts;
using RentTeslaServer.DataAccessLayer.Entities;
using RentTeslaServer.DomainLayer.Contracts;
using RentTeslaServer.DomainLayer.ModelDtos;
using RentTeslaServer.Exceptions;


namespace DomainLayer.Services
{
    public class CarService : ICarService
    {
        private readonly ILogger<CarService> _logger;
        private readonly IMapper _mapper;
        private readonly ICarRepository _carRepository;
        private readonly ICarTypeRepository _carTypeRepository;
        private readonly ICarRentalRepository _carRentalRepository;

        public CarService(ILogger<CarService> logger, IMapper mapper, ICarRepository carRepository, 
                            ICarTypeRepository carTypeRepository, ICarRentalRepository carRentalRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _carRepository = carRepository;
            _carTypeRepository = carTypeRepository;
            _carRentalRepository = carRentalRepository;
        }

        public async Task<IEnumerable<CarDto>> GetAllCarsInDataRange(string carRentalName, SearchDataDto searchDataDto)
        {
            if (carRentalName != searchDataDto.NamePickUp)
            {
                throw new BadRequestException("carRentalName and NamePickUp are diffrent!");
            }
          
            var cars = await _carRepository.GetCarsByFunction(searchDataDto.NamePickUp);

            var carsReady = cars.Where(x => x.Reservations == null || x.Reservations.Count == 0 ||
                                       CheckReservation(x.Reservations, searchDataDto)).ToList();

            if (carsReady.Count == 0)
                throw new NotFoundException("Car not found");
            var carsDto = _mapper.Map<List<CarDto>>(carsReady.DistinctBy(x => x.CarTypeId));
            return carsDto;
        }

        private static bool CheckReservation(List<Reservation> reservations, SearchDataDto searchDataDto)
        {
            return reservations.All(x => x.PickUpDate < searchDataDto.StartDate && x.ReturnDate < searchDataDto.StartDate ||
                                          x.PickUpDate > searchDataDto.EndDate && x.ReturnDate > searchDataDto.EndDate);

        }

        public async Task<IEnumerable<CarManagmentDto>> GetAllCars()
        {
            var cars = await _carRepository.GetAllCarsAsync();
            var carsDto = _mapper.Map<List<CarManagmentDto>>(cars);
            return carsDto;
        }

        public async Task<CarManagmentDetailDto> GetById(int Id)
        {
            var car = await _carRepository.GetCarAsyncById(Id) ?? throw new NotFoundException("Car with this Id not exist");
            var carDto = _mapper.Map<CarManagmentDetailDto>(car);
            return carDto;
        }
        public async Task Created(CarManagmentCreatedDto carManagmentCreatedDto)
        {
            if (await _carRepository.CheckPlates(carManagmentCreatedDto.Plates))
            {
                throw new BadRequestException("Plates has to be unique");
            }

            var car = _mapper.Map<Car>(carManagmentCreatedDto);
            await _carRepository.AddCarAsync(car);
        }

        public async Task Update(int id, CarManagmentDetailDto carManagmentDetailDto)
        {

            if (id != carManagmentDetailDto.Id) throw new BadRequestException("Wrong Id");
            
            var carType = await _carTypeRepository.GetCarTypeById(carManagmentDetailDto.CarTypeDto.Id) ?? throw new NotFoundException("Car Type not found");
            var car = _mapper.Map<Car>(carManagmentDetailDto);
            car.CarType = carType;
            var carRental = await _carRentalRepository.GetCarRentalByIdAsync( carManagmentDetailDto.CarRentalDto.Id) ?? throw new NotFoundException("Carrental not found");
            car.CarRental = carRental;

            _carRepository.UpdateCar(car);
        }

        public async Task Delete(int id)
        {
            _logger.LogInformation($"Car with id: {id} DELETE action invoked");

            var car = await _carRepository.GetCarAsyncById(id) ?? throw new NotFoundException("Car not found");
            _carRepository.DeleteCar(car);
        }

    }
}
