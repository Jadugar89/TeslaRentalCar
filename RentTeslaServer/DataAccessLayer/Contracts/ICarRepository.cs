using RentTeslaServer.DataAccessLayer.Entities;

namespace RentTeslaServer.DataAccessLayer.Contracts
{
    public interface ICarRepository
    {
        Task AddCarAsync(Car car);
        Task<bool> CheckPlates(string plates);
        void DeleteCar(Car car);
        Task<List<Car>> GetAllCarsAsync();
        Task<Car?> GetCarAsyncById(int id);
        Task<List<Car>> GetCarsByFunction(string name);
        void UpdateCar(Car car);
    }
}