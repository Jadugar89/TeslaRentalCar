using Microsoft.AspNetCore.Mvc;
using RentTeslaServer.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RentTeslaServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarRentalController : ControllerBase
    {
        private readonly IRentalCarService rentalCarService;

        public CarRentalController(IRentalCarService rentalCarService)
        {
            this.rentalCarService = rentalCarService;
        }


        // GET api/<CarRentalController>/5
        [HttpGet("{name}")]
        public async Task<IActionResult> Get(string name)
        {
            var result = await rentalCarService.SearchLocalization(name);
            return Ok(result);
        }


        // POST api/<CarRentalController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CarRentalController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CarRentalController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
