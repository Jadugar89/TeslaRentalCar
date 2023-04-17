using RentTeslaServer.Domain_Layer.ModelDtos;

namespace RentTeslaServer.Domain_Layer.Services
{
    public interface ICarTypeService
    {
        Task<IEnumerable<CarTypeDto>> GetAll();
        Task<CarTypeDto> GetById(int id);
        Task<CarTypeDto> GetByName(string name);
    }
}