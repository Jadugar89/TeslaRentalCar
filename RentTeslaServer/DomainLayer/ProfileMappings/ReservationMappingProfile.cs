using AutoMapper;
using RentTeslaServer.DataAccessLayer.Entities;
using RentTeslaServer.DomainLayer.ModelDtos;

namespace RentTeslaServer.DomainLayer.ProfileMappings
{
    public class ReservationMappingProfile: Profile
    {
        public ReservationMappingProfile()
        {
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
