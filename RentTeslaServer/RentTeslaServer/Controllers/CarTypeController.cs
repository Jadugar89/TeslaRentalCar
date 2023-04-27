using Microsoft.AspNetCore.Mvc;
using RentTeslaServer.DomainLayer.Contracts;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RentTeslaServer.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarTypeController : ControllerBase
    {
        private readonly ICarTypeService carTypeService;

        public CarTypeController(ICarTypeService carTypeService)
        {
            this.carTypeService = carTypeService;
        }
        // GET: api/<CarTypeController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await carTypeService.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await carTypeService.GetById(id);
            return Ok(result);
        }
        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var result = await carTypeService.GetByName(name);
            return Ok(result);
        }

        // POST api/<CarTypeController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CarTypeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CarTypeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
