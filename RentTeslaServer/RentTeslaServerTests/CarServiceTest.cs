using AutoMapper;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Logging;
using Moq;
using RentTeslaServer.DataAccessLayer;
using RentTeslaServer.Domain_Layer.ModelDtos;
using RentTeslaServer.Domain_Layer.ProfileMappings;
using RentTeslaServer.Domain_Layer.Services;
using RentTeslaServer.Services;
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
            carService = new CarService(logger, mapper, context);
        }
       

        [Theory]
        [MemberData(nameof(ReservationData))]
        public async Task GetCars(SearchDataDto searchDataDto)
        {
            var cars = await carService.GetAllCarsInDataRange(searchDataDto);
            Assert.True(cars.Count()==4);

        }

        public static IEnumerable<object[]> ReservationData()
        {

            yield return new object[]
            {
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
