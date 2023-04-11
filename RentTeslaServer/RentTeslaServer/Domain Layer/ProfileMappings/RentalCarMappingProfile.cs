﻿using AutoMapper;
using RentTeslaServer.DataAccessLayer.Entities;
using RentTeslaServer.Domain_Layer.ModelDtos;

namespace RentTeslaServer.Domain_Layer.ProfileMappings
{
    public class RentalCarMappingProfile: Profile
    {
        public RentalCarMappingProfile()
        {
            CreateMap<Car, CarDto>()
                .ForMember(m => m.Name,  c => c.MapFrom(s => s.CarType.Name))
                .ForMember(m => m.Motor, c => c.MapFrom(s => s.CarType.Motor))
                .ForMember(m => m.Range, c => c.MapFrom(s => s.CarType.Range))
                .ForMember(m => m.Seats, c => c.MapFrom(s => s.CarType.Seats));
            CreateMap<CarDto, Car>();
            CreateMap<ReservationCreateDto, Reservation>()
                .ForMember(m => m.Email, x => x.MapFrom(s => s.Email))
                .ForMember(m => m.PickUpDate, x => x.MapFrom(s => s.Reservation.StartDate))
                .ForMember(m => m.ReturnDate, x => x.MapFrom(s => s.Reservation.EndDate))
                .ForMember(m => m.Cost, x => x.MapFrom(s => s.Car.TotalCost));
            
            CreateMap<Reservation, SearchDataDto>();
            CreateMap<Reservation, History>()
                .ForMember(m => m.PickUpLocation, x => x.MapFrom(s => s.PickUpLocation.Name))
                .ForMember(m => m.ReturnLocation, x => x.MapFrom(s => s.ReturnLocation.Name))
                .ForMember(m => m.Car, x => x.MapFrom(s => s.Car));
          
        }
    }
}