using Microsoft.AspNetCore.Mvc;
using RentTeslaServer.Domain_Layer.ModelDtos;
using RentTeslaServer.Services;

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
           var result= await carService.GetAllCarsInDataRange(searchDataDto);
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
        [HttpPost]
        public void Post([FromBody] string value)
        {
          
        }

        // PUT api/<CarController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CarController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
