using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using RentTeslaServer.DataAccessLayer;
using RentTeslaServer.DataAccessLayer.Entities;
using RentTeslaServer.Domain_Layer.ModelDtos;
using RentTeslaServer.Exceptions;
using System.Collections.Generic;

namespace RentTeslaServer.Services
{
    public class CarService : ICarService
    {
        private readonly ILogger<CarService> logger;
        private readonly IMapper mapper;
        private readonly RentTeslaDbContext dbContext;

        public CarService(ILogger<CarService> logger, IMapper mapper, RentTeslaDbContext dbContext)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<CarDto>> GetAllCarsInDataRange(SearchDataDto searchDataDto)
        {
            var cars = await dbContext.Cars.Where(x => (x.CarRental.Name == searchDataDto.NamePickUp || (x.Reservations.Any(c => c.ReturnLocation.Name == searchDataDto.NamePickUp))) && x.IsPrepared)
                .Include(x => x.CarType)
                .Include(x => x.Reservations)
                .ToListAsync();

            var carsReady = cars.Where(x => x.Reservations == null || x.Reservations.Count() == 0 ||
                                       CheckReservation(x.Reservations, searchDataDto)).ToList();

            if (carsReady.Count() == 0)
                throw new NotFoundException("Car not found");
            var carsDto = mapper.Map<List<CarDto>>(carsReady.DistinctBy(x => x.CarTypeId));
            return carsDto;
        }

        private bool CheckReservation(List<Reservation> reservations, SearchDataDto searchDataDto)
        {
            return reservations.All(x => (x.PickUpDate < searchDataDto.StartDate && x.ReturnDate < searchDataDto.StartDate) ||
                                          (x.PickUpDate > searchDataDto.EndDate && x.ReturnDate > searchDataDto.EndDate));

        }

        public async Task<IEnumerable<CarManagmentDto>> GetAllCars()
        {
            var cars = await dbContext.Cars.Include(x => x.CarType).Include(x => x.CarRental).ToListAsync();
            var carsDto = mapper.Map<List<CarManagmentDto>>(cars);
            return carsDto;
        }

        public async Task<CarManagmentDetailDto> GetById(int Id)
        {
            var car = await dbContext.Cars
                .Include(x => x.CarType)
                .Include(x => x.CarRental)
                .FirstOrDefaultAsync(x => x.Id == Id);
            if (car == null)
                throw new NotFoundException("Car with this Id not exist");

            var carDto = mapper.Map<CarManagmentDetailDto>(car);
            return carDto;
        }
        public async Task Update(int id, CarManagmentDetailDto carManagmentDetailDto)
        {

            if (id != carManagmentDetailDto.Id) throw new BadRequestException("Wrong Id");
            var car = mapper.Map<Car>(carManagmentDetailDto);
            var carType = await dbContext.CarTypes.FirstOrDefaultAsync(x => x.Name == carManagmentDetailDto.CarTypeDto.Name);
            if (carType == null)
                throw new NotFoundException("Car Type not found");

            car.CarType = carType;
            var carRental = await dbContext.CarRentals.FirstOrDefaultAsync(x => x.Name == carManagmentDetailDto.CarRentalDto.Name);
            if (carRental == null) throw new NotFoundException("Carrental not found");
            car.CarRental = carRental;

            dbContext.Cars.Update(car);
            dbContext.SaveChanges();
        }

        public async Task Delete(int id)
        {
            logger.LogError($"Restaurant with id: {id} DELETE action invoked");

            var restaurant = await dbContext
                .Cars
                .FirstOrDefaultAsync(r => r.Id == id);

            if (restaurant is null)
                throw new NotFoundException("Car not found");


            dbContext.Cars.Remove(restaurant);
            dbContext.SaveChanges();

        }

    }
}
