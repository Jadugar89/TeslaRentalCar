using RentTeslaServer.Domain_Layer.ModelDtos;

namespace RentTeslaServer.Services
{
    public interface ICarService
    {
        Task<IEnumerable<CarManagmentDto>> GetAllCars();
        Task<IEnumerable<CarDto>> GetAllCarsInDataRange(SearchDataDto searchDataDto);
        Task<CarManagmentDetailDto> GetById(int Id);
    }
}