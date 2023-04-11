using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RentTeslaServer.DataAccessLayer;
using RentTeslaServer.DataAccessLayer.Entities;
using RentTeslaServer.Domain_Layer.ModelDtos;
using RentTeslaServer.Exceptions;

namespace RentTeslaServer.Services
{
    public class ReservationService : IReservationService
    {
        private readonly ILogger<ReservationService> logger;
        private readonly IMapper mapper;
        private readonly RentTeslaDbContext dbContext;

        public ReservationService(ILogger<ReservationService> logger, IMapper mapper, RentTeslaDbContext dbContext)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.dbContext = dbContext;
        }

        public async Task<Guid> CreateReservation(ReservationCreateDto reservationCreateDto)
        {
            var car = await dbContext.Cars.Include(x => x.CarRental).Include(x=>x.Reservations).FirstOrDefaultAsync(x => x.Id == reservationCreateDto.Car.Id);
            if (car == null)
            {
                throw new NotFoundException("Car not exist");
            }
            if (car.Reservations != null && !car.Reservations.All(x => x.ReturnDate < reservationCreateDto.Reservation.StartDate
                                 && x.PickUpDate < reservationCreateDto.Reservation.EndDate))
            {
                throw new BadRequestException("The car for these dates is already booked!");
            }
            var reservation = mapper.Map<Reservation>(reservationCreateDto);

            var localization = await dbContext.CarRentals.FirstOrDefaultAsync(x => x.Name == reservationCreateDto.Reservation.NamePickUp);

            if (localization == null)
            {
                throw new NotFoundException("Pick up localization not exist");
            }
            reservation.PickUpLocation = localization;

            if (reservationCreateDto.Reservation.NamePickUp.Equals(reservationCreateDto.Reservation.NameDropOff))
            {

                reservation.ReturnLocation = localization;
            }
            else
            {
                var localizationNameDropOff = await dbContext.CarRentals.FirstOrDefaultAsync(x => x.Name == reservationCreateDto.Reservation.NameDropOff);
                if (localizationNameDropOff == null)
                {
                    throw new NotFoundException("Drop off localization not exist");
                }
                reservation.ReturnLocation = localizationNameDropOff;
            }

            reservation.Car = car;
            reservation.Guid = Guid.NewGuid();
            await dbContext.Reservations.AddAsync(reservation);
            await dbContext.SaveChangesAsync();
            return reservation.Guid;

        }

        public void GetReservation()
        {

        }
    }
}
