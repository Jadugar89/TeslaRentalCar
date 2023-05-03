using DomainLayer.ModelDtos;
using DomainLayer.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentTeslaServer.DomainLayer.Contracts;

namespace RentTeslaServer.Api
{
    [Route("api/")]
    [ApiController]
    [Authorize]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;
        private readonly IValidator<CarManagmentCreatedDto> _createValidator;
        private readonly IValidator<CarManagmentDetailDto> _updateValidator;

        public CarController(ICarService carService, IValidator<CarManagmentCreatedDto> createValidator,IValidator<CarManagmentDetailDto> updateValidator)
        {
            _carService = carService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }
        [AllowAnonymous]
        [HttpGet("carrental/{carRentalName}/car")]
        [ResponseCache(Duration = 1800)]
        public async Task<IActionResult> GetCars([FromRoute] string carRentalName, [FromQuery] SearchDataDto searchDataDto)
        {
            var result = await _carService.GetAllCarsInDataRange(carRentalName, searchDataDto);
            return Ok(result);
        }

        [HttpGet("car")]
        [ResponseCache(Duration =1800)]
        public async Task<IActionResult> GetAllCars()
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
            var result = _createValidator.Validate(carManagmentCreatedDto);
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
            var result = _updateValidator.Validate(carManagmentDetailDto);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }
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
