using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentTeslaServer.DomainLayer.Contracts;

namespace RentTeslaServer.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CarRentalController : ControllerBase
    {
        private readonly IRentalCarService _rentalCarService;

        public CarRentalController(IRentalCarService rentalCarService)
        {
            _rentalCarService = rentalCarService;
        }

        [HttpGet("{name}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(string name)
        {
            var result = await _rentalCarService.SearchLocalization(name);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _rentalCarService.GetAll();
            return Ok(result);
        }
    }
}
