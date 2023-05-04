using Microsoft.Extensions.Logging;
using AutoMapper;
using RentTeslaServer.DomainLayer.ModelDtos;
using RentTeslaServer.DataAccessLayer.Contracts;
using RentTeslaServer.DataAccessLayer.Entities;
using RentTeslaServer.DomainLayer.Contracts;
using RentTeslaServer.Exceptions;

namespace DomainLayer.Services
{
    public class ReservationService : IReservationService
    {
        private readonly ILogger<ReservationService> _logger;
        private readonly IMapper _mapper;
        private readonly IReservationRepository _reservationRepository;
        private readonly ICarRentalRepository _carRentalRepository;
        private readonly ICarRepository _carRepository;

        public ReservationService(ILogger<ReservationService> logger, IMapper mapper,
                                  IReservationRepository reservationRepository,
                                  ICarRentalRepository carRentalRepository,
                                  ICarRepository carRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _reservationRepository = reservationRepository;
            _carRentalRepository = carRentalRepository;
            _carRepository = carRepository;
        }

        public async Task<Guid> CreateReservation(ReservationCreateDto reservationCreateDto)
        {
            _logger.LogInformation("Create reservation for data" + reservationCreateDto);

            var car = await _carRepository.GetCarAsyncById(reservationCreateDto.Car.Id) ?? throw new NotFoundException("Car not exist");
            
            if (car.Reservations != null && !car.Reservations.All(x => x.ReturnDate < reservationCreateDto.Reservation.StartDate
                                 && x.PickUpDate < reservationCreateDto.Reservation.EndDate))
            {
                throw new BadRequestException("The car for these dates is already booked!");
            }
           

            var localization = await _carRentalRepository.GetCarRentalByNameAsync(reservationCreateDto.Reservation.NamePickUp) ?? throw new NotFoundException("Pick up localization not exist");
            
            var reservation = _mapper.Map<Reservation>(reservationCreateDto);
            reservation.PickUpLocation = localization;

            if (reservationCreateDto.Reservation.NamePickUp.Equals(reservationCreateDto.Reservation.NameDropOff))
            {

                reservation.ReturnLocation = localization;
            }
            else
            {
                var localizationNameDropOff = await _carRentalRepository.GetCarRentalByNameAsync(reservationCreateDto.Reservation.NameDropOff) ?? throw new NotFoundException("Drop off localization not exist");
                reservation.ReturnLocation = localizationNameDropOff;
            }

            reservation.Car = car;
            reservation.Guid = Guid.NewGuid();
            await _reservationRepository.CreateReservation(reservation);   
            return reservation.Guid;
        }
    }
}
