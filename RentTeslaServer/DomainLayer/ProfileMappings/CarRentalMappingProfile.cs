using AutoMapper;
using RentTeslaServer.DomainLayer.ModelDtos;
using RentTeslaServer.DataAccessLayer.Entities;

namespace RentTeslaServer.DomainLayer.ProfileMappings
{
    public class CarRentalMappingProfile : Profile
    {
        public CarRentalMappingProfile()
        {
            CreateMap<CarRental, CarRentalDto>().ReverseMap();
            CreateMap<CreatedCarRentalDto, CarRental>();
        }
    }
}
