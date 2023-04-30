using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using RentTeslaServer.DataAccessLayer;
using RentTeslaServer.DataAccessLayer.Entities;
using RentTeslaServer.DomainLayer.Contracts;
using RentTeslaServer.DomainLayer.ModelDtos;
using RentTeslaServer.Exceptions;
using RentTeslaServer.Service.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace RentTeslaServer.DomainLayer.Services
{
    public class AuthService : IAuthService
    {

        private readonly RentTeslaDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authentication;

        public AuthService(RentTeslaDbContext dBContext, IPasswordHasher<User> passwordHasher, AuthenticationSettings authentication)
        {
            _dbContext = dBContext;
            _passwordHasher = passwordHasher;
            _authentication = authentication;
        }

        public string GenerateJwt(LoginDto loginDto)
        {

            var user = _dbContext.Users
                .FirstOrDefault(x => x.Email == loginDto.Email) ?? throw new BadRequestException("Invalid Email or Password");
            
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid Email or Password");
            }
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.LastName),
                new Claim(ClaimTypes.GivenName,user.Name),
                new Claim(ClaimTypes.Email,loginDto.Email),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authentication.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authentication.JwtExpireDays);

            var token = new JwtSecurityToken(
                 _authentication.JwtIssuer,
                 _authentication.JwtIssuer,
                 claims,
                 expires: expires,
                 signingCredentials: cred
                );
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
