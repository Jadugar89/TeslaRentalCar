using AutoMapper;
using DomainLayer.ModelDtos;
using DomainLayer.ProfileMappings;
using DomainLayer.Services;
using Microsoft.Extensions.Logging;
using Moq;
using RentTeslaServer.DataAccessLayer;
using RentTeslaServer.DataAccessLayer.Repository;
using FluentAssertions;
using RentTeslaServerTests.ServicesTests.Fixture;
using RentTeslaServer.DomainLayer.Contracts;
using RentTeslaServer.Exceptions;

namespace RentTeslaServerTests.ServicesTests
{
    [Collection("Database collection")]
    public class CarServiceTests 
    {
        private readonly RentTeslaDbContext _context;
        private readonly CarService _carService;

        private readonly IMapper mapper;

        public CarServiceTests(DataBaseFixture carRentalsDataBase)
        {
            _context = carRentalsDataBase.Context;
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new RentalCarMappingProfile());
            });
            mapper = mapperConfig.CreateMapper();

            var loggerMock = new Mock<ILogger<CarService>>();
            var carRepository = new CarRepository(_context);
            var carRentalsRepository = new CarRentalRepository(_context);
            var carTypeRepository = new CarTypeRepository(_context);
            _carService = new CarService(loggerMock.Object, mapper, carRepository, carTypeRepository, carRentalsRepository);
        }


        [Theory]
        [MemberData(nameof(ReservationData))]
        public async Task GetAllCarsInDataRange(string name, SearchDataDto searchDataDto)
        {
            // act
            var cars = await _carService.GetAllCarsInDataRange(name, searchDataDto);

            //assest

            cars.Should().NotBeNull();
            cars.Should().HaveCount(4);
        }
        [Theory]
        [InlineData(1)]
        public async Task GetCarById(int id)
        {
            //act
            var car=  await _carService.GetById(id);
            //assert
            car.Should().NotBeNull();
            car.Id.Should().Be(id);
        }

        [Fact]
        public async Task GetCarById_ReturnException()
        {
            //arrange
            var id = -12;
            //act
            Func<Task> act =  _carService.Awaiting(x=>x.GetById(id));
            //assert
            await act.Should().ThrowAsync<NotFoundException>()
                        .WithMessage("Car with this Id not exist");
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
