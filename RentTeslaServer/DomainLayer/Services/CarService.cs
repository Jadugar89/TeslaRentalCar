using AutoMapper;
using DomainLayer.ModelDtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RentTeslaServer.DataAccessLayer;
using RentTeslaServer.DataAccessLayer.Contracts;
using RentTeslaServer.DataAccessLayer.Entities;
using RentTeslaServer.DomainLayer.Contracts;
using RentTeslaServer.Exceptions;
using System;

namespace DomainLayer.Services
{
    public class CarService : ICarService
    {
        private readonly ILogger<CarService> logger;
        private readonly IMapper mapper;
        private readonly ICarRepository carRepository;
        private readonly ICarTypeRepository carTypeRepository;
        private readonly ICarRentalRepository carRentalRepository;

        public CarService(ILogger<CarService> logger, IMapper mapper, ICarRepository carRepository, 
                            ICarTypeRepository carTypeRepository, ICarRentalRepository carRentalRepository)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.carRepository = carRepository;
            this.carTypeRepository = carTypeRepository;
            this.carRentalRepository = carRentalRepository;
        }

        public async Task<IEnumerable<CarDto>> GetAllCarsInDataRange(string carrentalName, SearchDataDto searchDataDto)
        {
            if (carrentalName != searchDataDto.NamePickUp)
            {
                throw new BadRequestException("carrentalName and NamePickUp are diffrent!");
            }
          
            var cars = await carRepository.GetCarsByFunction(searchDataDto.NamePickUp);

            var carsReady = cars.Where(x => x.Reservations == null || x.Reservations.Count() == 0 ||
                                       CheckReservation(x.Reservations, searchDataDto)).ToList();

            if (carsReady.Count() == 0)
                throw new NotFoundException("Car not found");
            var carsDto = mapper.Map<List<CarDto>>(carsReady.DistinctBy(x => x.CarTypeId));
            return carsDto;
        }

        private bool CheckReservation(List<Reservation> reservations, SearchDataDto searchDataDto)
        {
            return reservations.All(x => x.PickUpDate < searchDataDto.StartDate && x.ReturnDate < searchDataDto.StartDate ||
                                          x.PickUpDate > searchDataDto.EndDate && x.ReturnDate > searchDataDto.EndDate);

        }

        public async Task<IEnumerable<CarManagmentDto>> GetAllCars()
        {
            var cars = await carRepository.GetAllCarsAsync();
            var carsDto = mapper.Map<List<CarManagmentDto>>(cars);
            return carsDto;
        }

        public async Task<CarManagmentDetailDto> GetById(int Id)
        {
            var car = await carRepository.GetCarAsyncById(Id);
            if (car == null)
                throw new NotFoundException("Car with this Id not exist");

            var carDto = mapper.Map<CarManagmentDetailDto>(car);
            return carDto;
        }
        public async Task Created(CarManagmentCreatedDto carManagmentCreatedDto)
        {
            if (await carRepository.CheckPlates(carManagmentCreatedDto.Plates))
            {
                throw new BadRequestException("Plates has to be unique");
            }

            var car = mapper.Map<Car>(carManagmentCreatedDto);
            await carRepository.AddCarAsync(car);
        }

        public async Task Update(int id, CarManagmentDetailDto carManagmentDetailDto)
        {

            if (id != carManagmentDetailDto.Id) throw new BadRequestException("Wrong Id");
            var car = mapper.Map<Car>(carManagmentDetailDto);
            var carType = await carTypeRepository.GetCarTypeById(carManagmentDetailDto.CarTypeDto.Id);
            if (carType == null)
                throw new NotFoundException("Car Type not found");

            car.CarType = carType;
            var carRental = await carRentalRepository.GetCarRentalByIdAsync( carManagmentDetailDto.CarRentalDto.Id);
            if (carRental == null) throw new NotFoundException("Carrental not found");
            car.CarRental = carRental;

            carRepository.UpdateCar(car);
        }

        public async Task Delete(int id)
        {
            logger.LogError($"Car with id: {id} DELETE action invoked");

            var car = await carRepository.GetCarAsyncById(id);

            if (car is null)
                throw new NotFoundException("Car not found");

            carRepository.DeleteCar(car);
        }

    }
}
