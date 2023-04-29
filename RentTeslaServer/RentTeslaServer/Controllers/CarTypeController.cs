using DomainLayer.Services;
using Microsoft.AspNetCore.Mvc;
using RentTeslaServer.DomainLayer.Contracts;

namespace RentTeslaServer.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarTypeController : ControllerBase
    {
        private readonly ICarTypeService _carTypeService;

        public CarTypeController(ICarTypeService carTypeService)
        {
            _carTypeService = carTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _carTypeService.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _carTypeService.GetById(id);
            return Ok(result);
        }
        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var result = await _carTypeService.GetByName(name);
            return Ok(result);
        }
    }
}
