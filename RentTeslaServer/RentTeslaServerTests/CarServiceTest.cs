using AutoMapper;
using DomainLayer.ModelDtos;
using DomainLayer.ProfileMappings;
using DomainLayer.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Logging;
using Moq;
using RentTeslaServer.DataAccessLayer;
using RentTeslaServer.DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentTeslaServerTests
{
    public class CarServiceTest : IClassFixture<CarRentalsDataBase>
    {
        private readonly RentTeslaDbContext context;
        private readonly CarService carService;

        private readonly IMapper mapper;

        public CarServiceTest(CarRentalsDataBase carRentalsDataBase)
        {
            this.context = carRentalsDataBase.Context;
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new RentalCarMappingProfile());
            });
            mapper = mapperConfig.CreateMapper();

            var loggerMock = new Mock<ILogger<CarService>>();
            var logger = loggerMock.Object;
            var carRepository = new CarRepository(context);
            var carRentalsRepository = new CarRentalRepository(context);
            var carTypeRepository = new CarTypeRepository(context);
            carService = new CarService(logger, mapper, carRepository, carTypeRepository,carRentalsRepository);
        }
       

        [Theory]
        [MemberData(nameof(ReservationData))]
        public async Task GetCars(string name,SearchDataDto searchDataDto)
        {
            var cars = await carService.GetAllCarsInDataRange(name,searchDataDto);
            Assert.True(cars.Count()==4);

        }

        public static IEnumerable<object[]> ReservationData()
        {

            yield return new object[]
            {
                     "Palma Airport",
                     new SearchDataDto()
                     {
                         NamePickUp="Palma Airport",
                         NameDropOff ="Palma Airport",
                         StartDate=new DateTime(2023, 4, 16, 0, 0, 0),
                         EndDate=new DateTime(2023, 4, 20, 0, 0, 0)
                     }
            };
        }
    }
}
