using DomainLayer.ModelDtos;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentTeslaServer.DomainLayer.Contracts;
using System;

namespace RentTeslaServer.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService reservationService;
        private readonly IValidator<ReservationCreateDto> validator;

        public ReservationController(IReservationService reservationService, IValidator<ReservationCreateDto> validator)
        {
            this.reservationService = reservationService;
            this.validator = validator;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ReservationCreateDto reservationCreateDto)
        {
            ValidationResult result = await validator.ValidateAsync(reservationCreateDto);

            if (!result.IsValid)
            {
                // Copy the validation results into ModelState.
                // ASP.NET uses the ModelState collection to populate 
                // error messages in the View.
                result.AddToModelState(this.ModelState);
                throw new ValidationException(result.Errors);
            }

            var resNumber = await reservationService.CreateReservation(reservationCreateDto);

            return Ok(resNumber);
        }

    }
}
