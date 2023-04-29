using Microsoft.AspNetCore.Mvc;
using RentTeslaServer.DomainLayer.Contracts;

namespace RentTeslaServer.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarRentalController : ControllerBase
    {
        private readonly IRentalCarService _rentalCarService;

        public CarRentalController(IRentalCarService rentalCarService)
        {
            _rentalCarService = rentalCarService;
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> Get(string name)
        {
            var result = await _rentalCarService.SearchLocalization(name);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _rentalCarService.GetAll();
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
