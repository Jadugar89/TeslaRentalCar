using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentTeslaServer.DomainLayer.Contracts;
using RentTeslaServer.DomainLayer.ModelDtos;
using System.Text.Json;

namespace RentTeslaServer.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginDto loginDto)
        {
            string token = this._authService.GenerateJwt(loginDto);
            string jsonString = JsonSerializer.Serialize(token);
            return Ok(jsonString);
        }
    }
}
