﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentTeslaServer.Domain_Layer.ModelDtos;
using RentTeslaServer.Domain_Layer.Validators;
using RentTeslaServer.Services;
using System.Reflection.Metadata.Ecma335;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RentTeslaServer.Controllers
{
    [Route("api/")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService carService;

        public CarController(ICarService carService)
        {
            this.carService = carService;
        }
        // GET: api/<CarController>
        [HttpGet("carrental/{carrentalName}/car")]
        public async Task<IActionResult> GetCars([FromRoute] string carrentalName, [FromQuery] SearchDataDto searchDataDto)
        {
           var result= await carService.GetAllCarsInDataRange(carrentalName,searchDataDto);
            return Ok(result);  
        }
        [HttpGet("car")]
        public async Task<IActionResult> GetCars()
        {
            var result = await carService.GetAllCars();
            return Ok(result);
        }

        [HttpGet("car/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await carService.GetById(id);
            return Ok(result);
        }

        // POST api/<CarController>
        [HttpPost("car")]
        public async Task<IActionResult> Post([FromBody] CarManagmentCreatedDto carManagmentCreatedDto)
        {
            var validator = new CarManagmentCreatedDtoValidator();
            var result = validator.Validate(carManagmentCreatedDto);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }
            await carService.Created(carManagmentCreatedDto);
            return Ok();
        }

        // PUT api/<CarController>/5
        [HttpPut("car/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CarManagmentDetailDto carManagmentDetailDto)
        {
            await carService.Update(id, carManagmentDetailDto);
            return NoContent();
        }

        // DELETE api/<CarController>/5
        [HttpDelete("car/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
           await carService.Delete(id);
           return NoContent();
        }
    }
}
