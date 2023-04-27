using DomainLayer.ModelDtos;

namespace RentTeslaServer.DomainLayer.Contracts
{
    public interface IRentalCarService
    {
        Task<IEnumerable<CarRentalDto>> GetAll();
        Task<IEnumerable<string>> SearchLocalization(string name);
    }
}