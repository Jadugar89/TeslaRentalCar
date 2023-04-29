using DomainLayer.ModelDtos;

namespace RentTeslaServer.DomainLayer.Contracts
{
    public interface ICarService
    {
        Task Created(CarManagmentCreatedDto carManagmentCreatedDto);
        Task Delete(int id);
        Task<IEnumerable<CarManagmentDto>> GetAllCars();
        Task<IEnumerable<CarDto>> GetAllCarsInDataRange(string carRentalName, SearchDataDto searchDataDto);
        Task<CarManagmentDetailDto> GetById(int Id);
        Task Update(int id, CarManagmentDetailDto carManagmentDetailDto);
    }
}