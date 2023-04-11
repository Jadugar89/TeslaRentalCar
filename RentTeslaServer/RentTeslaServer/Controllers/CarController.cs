using Microsoft.AspNetCore.Mvc;
using RentTeslaServer.Domain_Layer.ModelDtos;
using RentTeslaServer.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RentTeslaServer.Controllers
{
    [Route("api/carrental/{carrentalName}/car")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService carService;

        public CarController(ICarService carService)
        {
            this.carService = carService;
        }
        // GET: api/<CarController>
        [HttpGet]
        public async Task<IActionResult> GetCars([FromRoute] string carrentalName, [FromQuery] SearchDataDto searchDataDto)
        {
           var result= await carService.GetAllCars(searchDataDto);
            return Ok(result);  
        }

        // GET api/<CarController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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
