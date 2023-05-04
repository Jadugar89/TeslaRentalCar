using RentTeslaServer.DataAccessLayer.Entities;

namespace RentTeslaServer.DataAccessLayer.Contracts
{
    public interface ICarRentalRepository
    {
        Task AddCarRentalAsync(CarRental carRental);
        Task<bool> CheckName(string name);
        void DeleteCarRental(CarRental carRental);
        Task<List<CarRental>> GetAllCarRentalsAsync();
        Task<CarRental?> GetCarRentalByIdAsync(int id);
        Task<CarRental?> GetCarRentalByNameAsync(string name);
        Task<List<string>> GetLocalizationNameAsync(string name);
        void UpdateCarRental(CarRental carRental);
    }
}