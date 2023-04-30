using RentTeslaServer.DomainLayer.ModelDtos;

namespace RentTeslaServer.DomainLayer.Contracts
{
    public interface IAuthService
    {
        string GenerateJwt(LoginDto loginDto);
    }
}