using RentTeslaServer.Domain_Layer.ModelDtos;

namespace RentTeslaServer.Services
{
    public interface ICarService
    {
        Task Created(CarManagmentCreatedDto carManagmentCreatedDto);
        Task Delete(int id);
        Task<IEnumerable<CarManagmentDto>> GetAllCars();
        Task<IEnumerable<CarDto>> GetAllCarsInDataRange(string carrentalName, SearchDataDto searchDataDto);
        Task<CarManagmentDetailDto> GetById(int Id);
        Task Update(int id, CarManagmentDetailDto carManagmentDetailDto);
    }
}