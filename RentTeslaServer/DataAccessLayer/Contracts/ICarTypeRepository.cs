using RentTeslaServer.DataAccessLayer.Entities;

namespace RentTeslaServer.DataAccessLayer.Contracts
{
    public interface ICarTypeRepository
    {
        Task<List<CarType>> GetAllCarTypes();
        Task<CarType?> GetCarTypeById(int id);
        Task<CarType?> GetCarTypeByName(string name);
    }
}