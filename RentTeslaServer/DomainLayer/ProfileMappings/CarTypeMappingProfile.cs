using AutoMapper;
using RentTeslaServer.DomainLayer.ModelDtos;
using RentTeslaServer.DataAccessLayer.Entities;


namespace RentTeslaServer.DomainLayer.ProfileMappings
{
    public class CarTypeMappingProfile : Profile
    {
        public CarTypeMappingProfile()
        {
            CreateMap<CarType, CarTypeDto>().ReverseMap();
        }
    }
}
