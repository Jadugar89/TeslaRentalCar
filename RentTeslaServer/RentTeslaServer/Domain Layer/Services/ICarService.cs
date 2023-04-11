using RentTeslaServer.Domain_Layer.ModelDtos;

namespace RentTeslaServer.Services
{
    public interface ICarService
    {
        Task<IEnumerable<CarDto>> GetAllCars(SearchDataDto searchDataDto);
    }
}