using AutoMapper;
using RentTeslaServer.DomainLayer.ModelDtos;
using RentTeslaServer.DataAccessLayer.Entities;

namespace DomainLayer.ProfileMappings
{
    public class CarMappingProfile : Profile
    {
        public CarMappingProfile()
        {
            CreateMap<Car, CarDto>()
                .ForMember(m => m.Name, c => c.MapFrom(s => s.CarType.Name))
                .ForMember(m => m.Motor, c => c.MapFrom(s => s.CarType.Motor))
                .ForMember(m => m.Range, c => c.MapFrom(s => s.CarType.Range))
                .ForMember(m => m.Seats, c => c.MapFrom(s => s.CarType.Seats));

            CreateMap<Car, CarManagmentDetailDto>()
                .ForMember(m => m.CarRentalDto, x => x.MapFrom(s => s.CarRental))
                .ForMember(m => m.CarTypeDto, x => x.MapFrom(s => s.CarType));

            CreateMap<CarManagmentDetailDto, Car>();

            CreateMap<CarDto, Car>();

            CreateMap<CarManagmentCreatedDto, Car>()
                .ForMember(m => m.CarTypeId, c => c.MapFrom(s => s.CarTypeDto.Id))
                .ForMember(m => m.CarRentalId, c => c.MapFrom(s => s.CarRentalDto.Id));

            CreateMap<Car, CarManagmentDto>()
                .ForMember(m => m.Name, c => c.MapFrom(s => s.CarType.Name))
                .ForMember(m => m.CarRentalName, c => c.MapFrom(s => s.CarRental.Name))
                .ForMember(m => m.CarRentalCity, c => c.MapFrom(s => s.CarRental.City));
        }
    }
}
