using RentTeslaServer.DataAccessLayer.Entities;

namespace RentTeslaServer.DataAccessLayer.Contracts
{
    public interface ICarRentalRepository
    {
        Task<List<CarRental>> GetAllCarRentalsAsync();
        Task<CarRental?> GetCarRentalByIdAsync(int id);
        Task<CarRental?> GetCarRentalByNameAsync(string name);
        Task<List<string>> GetLocalizationNameAsync(string name);
    }
}