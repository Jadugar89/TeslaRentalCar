using Microsoft.EntityFrameworkCore;
using RentTeslaServer.DataAccessLayer;
using RentTeslaServer.DataAccessLayer.Entities;
using RentTeslaServer.Domain_Layer.ModelDtos;
using RentTeslaServer.Services;
using Moq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using RentTeslaServer.Domain_Layer.ProfileMappings;
using Microsoft.AspNetCore.Cors.Infrastructure;
using RentTeslaServer.Exceptions;
using RentTeslaServer.Domain_Layer.Services;
using NLog;

namespace RentTeslaServerTests
{

    public class DatabaseTest : IClassFixture<CarRentalsDataBase>
    {
        private readonly RentTeslaDbContext context;
        private readonly ReservationService reservationService;
        private readonly PurgeReservationService purgeReservationService;
        private readonly IMapper mapper;


        public DatabaseTest(CarRentalsDataBase carRentalsDataBase)
        {
            this.context = carRentalsDataBase.Context;
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new RentalCarMappingProfile());
            });
            mapper = mapperConfig.CreateMapper();

            var loggerMock = new Mock<ILogger<ReservationService>>();
            var logger = loggerMock.Object;
            reservationService = new ReservationService(logger, mapper, context);

            var loggerPRMock = new Mock<ILogger<PurgeReservationService>>();
            var loggerPR = loggerPRMock.Object;
            purgeReservationService = new PurgeReservationService(loggerPR, mapper, context);


        }
        [Theory]
        [MemberData(nameof(ReservationData))]
        public async Task AddReservationAsync(ReservationCreateDto reservationCreateDto)
        {
            var result = await reservationService.CreateReservation(reservationCreateDto);
            Assert.True(result!=Guid.Empty);
        }

        [Theory]
        [MemberData(nameof(ExceptionData))]
        public async Task CheckException(ReservationCreateDto reservationCreateDto)
        {
            var result = await Assert.ThrowsAsync<BadRequestException>(()=>  reservationService.CreateReservation(reservationCreateDto));
            Assert.NotNull(result);
        }
        [Fact]
        public async Task CheckPurgeReservation()
        {
            var expected = context.Reservations.Single(r => r.Email == "First@wp.pl");

            await purgeReservationService.MoveToHistory();

            Assert.DoesNotContain<Reservation>(expected, context.Reservations.ToList());
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
                         DailyPrice=51.40,
                         TotalCost=500
                    },
                     Reservation= new SearchDataDto()
                     {
                         NamePickUp="Palma Airport",
                         NameDropOff ="Palma City Center",
                         StartDate=new DateTime(2023, 4, 14, 0, 0, 0),
                         EndDate=new DateTime(2023, 4,20, 0, 0, 0)
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
                         DailyPrice=51.40,
                         TotalCost=500
                    },
                     Reservation= new SearchDataDto()
                     {
                         NamePickUp="Alcudia",
                         NameDropOff ="Palma Airport",
                         StartDate=new DateTime(2023, 4, 9, 0, 0, 0),
                         EndDate=new DateTime(2023, 4, 15, 0, 0, 0)
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
                         DailyPrice=51.40,
                         TotalCost=500
                    },
                     Reservation= new SearchDataDto()
                     {
                         NamePickUp="Palma Airport",
                         NameDropOff ="Palma Airport",
                         StartDate=new DateTime(2023, 4, 22, 0, 0, 0),
                         EndDate=new DateTime(2023, 4, 29, 0, 0, 0)
                     }
               },
            };
        }
    }
}
