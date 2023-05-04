using RentTeslaServer.DataAccessLayer;
using Moq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using RentTeslaServer.Exceptions;
using DomainLayer.Services;
using DomainLayer.ProfileMappings;
using RentTeslaServer.DomainLayer.ModelDtos;
using RentTeslaServer.DataAccessLayer.Repository;
using FluentAssertions;
using RentTeslaServerTests.ServicesTests.Fixture;

namespace RentTeslaServerTests.ServicesTests
{
    [Collection("Database collection")]
    public class DatabaseTest
    {
        private readonly RentTeslaDbContext _context;
        private readonly ReservationService _reservationService;
        private readonly PurgeReservationService _purgeReservationService;
        private readonly IMapper _mapper;


        public DatabaseTest(DataBaseFixture carRentalsDataBase)
        {
            _context = carRentalsDataBase.Context;
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CarMappingProfile());
            });
            _mapper = mapperConfig.CreateMapper();

            var loggerMock = new Mock<ILogger<ReservationService>>();
            var reservationRepo = new ReservationRepository(_context);
            var carRepo = new CarRepository(_context);
            var carRentalsRepository = new CarRentalRepository(_context);
            _reservationService = new ReservationService(loggerMock.Object, _mapper, reservationRepo, carRentalsRepository, carRepo);

            var loggerPRMock = new Mock<ILogger<PurgeReservationService>>();
            var loggerPR = loggerPRMock.Object;
            _purgeReservationService = new PurgeReservationService(loggerPR, _mapper, _context);


        }
        [Theory]
        [MemberData(nameof(ReservationData))]
        public async Task AddReservationAsync(ReservationCreateDto reservationCreateDto)
        {
            var result = await _reservationService.CreateReservation(reservationCreateDto);
            result.Should().NotBeEmpty();
        }

        [Theory]
        [MemberData(nameof(ExceptionData))]
        public async Task CheckException(ReservationCreateDto reservationCreateDto)
        {
            //act
            Func<Task> act = _reservationService.Awaiting(y => y.CreateReservation(reservationCreateDto));
            //assert
            await act.Should().ThrowAsync<BadRequestException>()
                        .WithMessage("The car for these dates is already booked!");
        }
        [Fact]
        public async Task CheckPurgeReservation()
        {
            // Arrange
            var expected = _context.Reservations.Single(r => r.Email == "First@wp.pl");
            // Act
            await _purgeReservationService.MoveToHistory();
            // Assert
            _context.Reservations.ToList().Should().NotContain(expected);
            
        }

        public static IEnumerable<object[]> ExceptionData()
        {
            yield return new object[]
            {
               new ReservationCreateDto()
               {
                    Email="Jasiu@wp.pl",
                    Car= new BookCarDto()
                    {
                         Id=1,
                         Name="Model S",
                         Motor ="1020",
                         Range=396,
                         Seats=5,
                         DailyPrice=51.40m,
                         TotalCost=500
                    },
                     Reservation= new SearchDataDto()
                     {
                         NamePickUp="Palma Airport",
                         NameDropOff ="Palma City Center",
                         StartDate=DateTime.Now.AddDays(14),
                         EndDate=DateTime.Now.AddDays(20)
                     }
               },
            };
            yield return new object[]
            {
               new ReservationCreateDto()
               {
                    Email="sdsa@wp.pl",
                    Car= new BookCarDto()
                    {
                         Id=14,
                         Name="Model S",
                         Motor ="1020",
                         Range=396,
                         Seats=5,
                         DailyPrice=51.40m,
                         TotalCost=500
                    },
                     Reservation= new SearchDataDto()
                     {
                         NamePickUp="Alcudia",
                         NameDropOff ="Palma Airport",
                         StartDate=DateTime.Now.AddDays(9),
                         EndDate=DateTime.Now.AddDays(15),
                     }
               },
          };
        }
        public static IEnumerable<object[]> ReservationData()
        {

            yield return new object[]
            {
               new ReservationCreateDto()
               {
                    Email="test@wp.pl",
                    Car= new BookCarDto()
                    {
                         Id=1,
                         Name="Model S",
                         Motor ="1020",
                         Range=396,
                         Seats=5,
                         DailyPrice=51.40m,
                         TotalCost=500
                    },
                     Reservation= new SearchDataDto()
                     {
                         NamePickUp="Palma Airport",
                         NameDropOff ="Palma Airport",
                         StartDate=DateTime.Now.AddDays(22),
                         EndDate=DateTime.Now.AddDays(29),
                     }
               },
            };
        }
    }
}
