using RentTeslaServer.DomainLayer.ModelDtos;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentTeslaServer.DomainLayer.Contracts;


namespace RentTeslaServer.Api
{
    /// <summary>
    /// This controller manages the cars.
    /// </summary>
    [Route("api/car")]
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

        /// <summary>
        /// Retrieves all the cars in a given data range for a specific car rental.
        /// </summary>
        /// <remarks>
        /// This API endpoint retrieves all the cars that are available for rent in a specified data range.
        /// </remarks>
        /// <param name="carRentalName">The name of the car rental.</param>
        /// <param name="searchDataDto">The search criteria for the car data range.</param>
        /// <returns>A list of cars available for rent in the specified date range.</returns>
        /// <response code="200">Returns a list of cars available for rent in the specified date range.</response>
        /// <response code="400">If the request is invalid.</response>
        
        [HttpGet("carsInDataRange/{carRentalName}")]
        [ResponseCache(Duration = 120)]
        [ProducesResponseType(typeof(List<CarDto>), 200)]
        [ProducesResponseType(400)]
        [Produces("application/json")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllCarsInDataRange([FromRoute] string carRentalName, [FromQuery] SearchDataDto searchDataDto)
        {
            var result = await _carService.GetAllCarsInDataRange(carRentalName, searchDataDto);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves all the cars available.
        /// </summary>
        /// <remarks>
        /// This API endpoint retrieves all the cars.
        /// </remarks>
        /// <returns>A list of cars available for rent.</returns>
        /// <response code="200">Returns a list of cars available.</response>
        /// <response code="400">If the request is invalid.</response>
        /// <response code="401">If the User is unauthorized.</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<CarDto>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("application/json")]
        public async Task<IActionResult> GetAllCars()
        {
            var result = await _carService.GetAllCars();
            return Ok(result);
        }

        /// <summary>
        /// Retrieves the details of a car by its ID.
        /// </summary>
        /// <param name="id">The ID of the car.</param>
        /// <returns>The details of the car.</returns>
        /// <response code="200">Returns the details of the car.</response>
        /// <response code="400">If the request is invalid.</response>
        /// <response code="401">If the User is unauthorized.</response>
        /// <response code="404">If the car with the specified ID is not found.</response>
        [HttpGet("getById/{id:int}")]
        [ProducesResponseType(typeof(CarDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _carService.GetById(id);
            return Ok(result);
        }
        /// <summary>
        /// Creates a new car.
        /// </summary>
        /// <remarks>
        /// This API endpoint creates a new car that will be added to the management system.
        /// </remarks>
        /// <param name="carManagmentCreatedDto">The details of the new car to be created.</param>
        /// <returns>A message indicating that the car was created successfully.</returns>
        /// <response code="200">Indicates that the car was created successfully.</response>
        /// <response code="400">If the request is invalid.</response>
        /// <response code="401">If the User is unauthorized.</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("application/json")]
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
        /// <summary>
        /// Updates the details of a car by its ID.
        /// </summary>
        /// <remarks>
        /// This API endpoint updates the details of a car with the specified ID.
        /// </remarks>
        /// <param name="id">The ID of the car to update.</param>
        /// <param name="carManagmentDetailDto">The details of the car to update.</param>
        /// <returns>No content if the update was successful.</returns>
        /// <response code="204">Indicates that the update was successful.</response>
        /// <response code="400">If the request is invalid.</response>
        /// <response code="401">If the User is unauthorized.</response>
        /// <response code="404">If the car with the specified ID is not found.</response>
        [HttpPut("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
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
        /// <summary>
        /// Deletes a car by its ID.
        /// </summary>
        /// <remarks>
        /// This API endpoint deletes a car with the specified ID.
        /// </remarks>
        /// <param name="id">The ID of the car to delete.</param>
        /// <returns>No content if the deletion was successful.</returns>
        /// <response code="204">Indicates that the deletion was successful.</response>
        /// <response code="401">If the User is unauthorized.</response>
        /// <response code="404">If the car with the specified ID is not found.</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public async Task<IActionResult> Delete(int id)
        {
            await _carService.Delete(id);
            return NoContent();
        }
    }
}
