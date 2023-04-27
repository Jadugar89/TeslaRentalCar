using AutoMapper;
using DomainLayer.ModelDtos;
using Microsoft.Extensions.Logging;
using RentTeslaServer.DataAccessLayer.Contracts;
using RentTeslaServer.DataAccessLayer.Entities;
using RentTeslaServer.DomainLayer.Contracts;
using RentTeslaServer.Exceptions;

namespace DomainLayer.Services
{
    public class ReservationService : IReservationService
    {
        private readonly ILogger<ReservationService> logger;
        private readonly IMapper mapper;
        private readonly IReservationRepository reservationRepository;
        private readonly ICarRentalRepository carRentalRepository;
        private readonly ICarRepository carRepository;

        public ReservationService(ILogger<ReservationService> logger, IMapper mapper,
                                  IReservationRepository reservationRepository,
                                  ICarRentalRepository carRentalRepository,
                                  ICarRepository carRepository)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.reservationRepository = reservationRepository;
            this.carRentalRepository = carRentalRepository;
            this.carRepository = carRepository;
        }

        public async Task<Guid> CreateReservation(ReservationCreateDto reservationCreateDto)
        {
            var car = await carRepository.GetCarAsyncById(reservationCreateDto.Car.Id);
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

            var localization = await carRentalRepository.GetCarRentalByNameAsync(reservationCreateDto.Reservation.NamePickUp);
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
                var localizationNameDropOff = await carRentalRepository.GetCarRentalByNameAsync(reservationCreateDto.Reservation.NameDropOff);
                if (localizationNameDropOff == null)
                {
                    throw new NotFoundException("Drop off localization not exist");
                }
                reservation.ReturnLocation = localizationNameDropOff;
            }

            reservation.Car = car;
            reservation.Guid = Guid.NewGuid();
            await reservationRepository.CreateReservation(reservation);   
            return reservation.Guid;
        }
    }
}
