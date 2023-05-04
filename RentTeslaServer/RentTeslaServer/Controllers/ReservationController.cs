using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using RentTeslaServer.DomainLayer.Contracts;
using RentTeslaServer.DomainLayer.ModelDtos;

namespace RentTeslaServer.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly IValidator<ReservationCreateDto> _validator;

        public ReservationController(IReservationService reservationService, IValidator<ReservationCreateDto> validator)
        {
            _reservationService = reservationService;
            _validator = validator;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ReservationCreateDto reservationCreateDto)
        {
            ValidationResult result = await _validator.ValidateAsync(reservationCreateDto);

            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var resNumber = await _reservationService.CreateReservation(reservationCreateDto);

            return Ok(resNumber);
        }

    }
}
