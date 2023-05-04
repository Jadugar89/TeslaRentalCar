using RentTeslaServer.DomainLayer.ModelDtos;

namespace RentTeslaServer.DomainLayer.Contracts
{
    public interface ICarTypeService
    {
        Task<IEnumerable<CarTypeDto>> GetAll();
        Task<CarTypeDto> GetById(int id);
        Task<CarTypeDto> GetByName(string name);
    }
}   