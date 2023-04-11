namespace RentTeslaServer.Services
{
    public interface IRentalCarService
    {
        Task<IEnumerable<string>> SearchLocalization(string Name);
    }
}