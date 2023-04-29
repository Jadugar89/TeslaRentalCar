using DomainLayer.ModelDtos;
using DomainLayer.Validators;
using Microsoft.AspNetCore.Mvc;
using RentTeslaServer.DomainLayer.Contracts;

namespace RentTeslaServer.Api
{
    [Route("api/")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet("carrental/{carRentalName}/car")]
        public async Task<IActionResult> GetCars([FromRoute] string carRentalName, [FromQuery] SearchDataDto searchDataDto)
        {
            var result = await _carService.GetAllCarsInDataRange(carRentalName, searchDataDto);
            return Ok(result);
        }

        [HttpGet("car")]
        public async Task<IActionResult> GetCars()
        {
            var result = await _carService.GetAllCars();
            return Ok(result);
        }

        [HttpGet("car/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _carService.GetById(id);
            return Ok(result);
        }

        [HttpPost("car")]
        public async Task<IActionResult> Post([FromBody] CarManagmentCreatedDto carManagmentCreatedDto)
        {
            var validator = new CarManagmentCreatedDtoValidator();
            var result = validator.Validate(carManagmentCreatedDto);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }
            await _carService.Created(carManagmentCreatedDto);
            return Ok();
        }

        [HttpPut("car/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CarManagmentDetailDto carManagmentDetailDto)
        {
            await _carService.Update(id, carManagmentDetailDto);
            return NoContent();
        }

        [HttpDelete("car/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _carService.Delete(id);
            return NoContent();
        }
    }
}
