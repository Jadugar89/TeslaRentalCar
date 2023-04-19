using RentTeslaServer.Domain_Layer.ModelDtos;

namespace RentTeslaServer.Services
{
    public interface IRentalCarService
    {
        Task<IEnumerable<CarRentalDto>> GetAll();
        Task<IEnumerable<string>> SearchLocalization(string name);
    }
}