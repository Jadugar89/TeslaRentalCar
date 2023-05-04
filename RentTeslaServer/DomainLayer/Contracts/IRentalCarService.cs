using RentTeslaServer.DomainLayer.ModelDtos;    


namespace RentTeslaServer.DomainLayer.Contracts
{
    public interface IRentalCarService
    {
        Task Created(CreatedCarRentalDto createdCarRentalDto);
        Task Delete(int id);
        Task<IEnumerable<CarRentalDto>> GetAll();
        Task<CarRentalDto> GetCarRentalDtoByIdAsync(int id);
        Task<IEnumerable<string>> SearchLocalization(string name);
        void Update(int id, CarRentalDto CarRentalDto);
    }
}