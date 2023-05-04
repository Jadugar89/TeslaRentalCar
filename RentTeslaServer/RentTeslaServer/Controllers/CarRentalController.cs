using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using RentTeslaServer.DomainLayer.ModelDtos;
using RentTeslaServer.DomainLayer.Contracts;

namespace RentTeslaServer.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CarRentalController : ControllerBase
    {
        private readonly IRentalCarService _rentalCarService;
        private readonly IValidator<CreatedCarRentalDto> _createValidator;
        private readonly IValidator<CarRentalDto> _updateValidator;

        public CarRentalController(IRentalCarService rentalCarService,
                                   IValidator<CreatedCarRentalDto> createValidator,
                                   IValidator<CarRentalDto> updateValidator)
        {
            _rentalCarService = rentalCarService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        /// <summary>
        /// Gets a list of names that match the given search string.
        /// </summary>
        /// <param name="name">The search string.</param>
        /// <returns>A list of matching names.</returns>
        /// <response code="200">Returns the list of matching names.</response>
        [HttpGet("GetNames/{name}")]
        [AllowAnonymous]
        [ResponseCache(Duration = 1800)]
        [ProducesResponseType(typeof(IEnumerable<string>), 200)]
        public async Task<IActionResult> GetNames(string name)
        {
            var result = await _rentalCarService.SearchLocalization(name);
            return Ok(result);
        }
        /// <summary>
        /// Retrieves all the Car Rentals.
        /// </summary>
        /// <remarks>
        /// This API endpoint retrieves all the car rentals.
        /// </remarks>
        /// <returns>A list of car rentals .</returns>
        /// <response code="200">Returns a list of car rentals.</response>
        /// <response code="400">If the request is invalid.</response>
        /// <response code="401">If the User is unauthorized.</response>
        [HttpGet("GetAllCarRentals")]
        [ProducesResponseType(typeof(List<CarRentalDto>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("application/json")]
        public async Task<IActionResult> GetAllCarRentals()
        {
            var result = await _rentalCarService.GetAll();
            return Ok(result);
        }

        /// <summary>
        /// Retrieves  the car rental by id.
        /// </summary>
        /// <remarks>
        /// This API endpoint retrieves the car rental by id.
        /// </remarks>
        /// <returns>CarRentalDto</returns>
        /// <response code="200">Returns carrental with id.</response>
        /// <response code="400">If the request is invalid.</response>
        /// <response code="401">If the User is unauthorized.</response>
        /// <response code="404">If the id is invalid.</response>
        [HttpGet("GetCarRentalById/{id}")]
        [ProducesResponseType(typeof(List<CarRentalDto>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public async Task<IActionResult> GetCarRentalById(int id)    
        {
            var result = await _rentalCarService.GetCarRentalDtoByIdAsync(id);
            return Ok(result);
        }
        /// <summary>
        /// Create a new car rental.
        /// </summary>
        /// <remarks>
        /// This API endpoint creates a new car rental that will be added to the management system.
        /// </remarks>
        /// <param name="createdCarRentalDto">The details of the new car to be created.</param>
        /// <returns>A message indicating that the car was created successfully.</returns>
        /// <response code="200">Indicates that the car was created successfully.</response>
        /// <response code="400">If the request is invalid.</response>
        /// <response code="401">If the User is unauthorized.</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("application/json")]
        public async Task<IActionResult> Post([FromBody] CreatedCarRentalDto createdCarRentalDto)
        {
            var result = _createValidator.Validate(createdCarRentalDto);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }
            await _rentalCarService.Created(createdCarRentalDto);
            return Ok();
        }
        /// <summary>
        /// Updates the details of a Car Rental by its ID.
        /// </summary>
        /// <remarks>
        /// This API endpoint updates the details of a Car Rental with the specified ID.
        /// </remarks>
        /// <param name="id">The ID of the Car Rental to update.</param>
        /// <param name="carRentalDto">The details of the car to update.</param>
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
        public IActionResult Put(int id, [FromBody] CarRentalDto carRentalDto)
        {
            var result = _updateValidator.Validate(carRentalDto);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }
             _rentalCarService.Update(id, carRentalDto);

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
            await _rentalCarService.Delete(id);
            return NoContent();
        }

    }
}
