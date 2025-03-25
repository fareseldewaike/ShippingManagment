using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shipping.core.Interfaces;
using Shipping.DTO;

namespace Shipping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;
        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }
        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<CityDTO>>> GetCities()
        {
            var cities = await _cityService.GetCities();
            if (cities == null || !cities.Any())
            {
                return NotFound(new { Status = 404, Message = "No cities found." });
            }
            return Ok(cities);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CityDTO>> GetCityById(int id)
        {
            var city = await _cityService.GetCityById(id);
            if (city == null)
            {
                return NotFound(new { Status = 404, Message = "City not found." });
            }
            return Ok(city);
        }
        [HttpPost]
        public async Task<ActionResult> AddCity(CityDTO cityDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _cityService.AddCity(cityDto);
            return Ok(new { Status = 201, Message = "City created successfully." });
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCity(int id, CityDTO cityDto)
        {
            var existingCity = await _cityService.GetCityById(id);
            if (existingCity == null)
            {
                return NotFound(new { Status = 404, Message = "City not found." });
            }
            await _cityService.UpdateCity(id, cityDto);
            return Ok(new { Status = 200, Message = "City updated successfully." });
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCity(int id)
        {
            var existingCity = await _cityService.GetCityById(id);
            if (existingCity == null)
            {
                return NotFound(new { Status = 404, Message = "City not found." });
            }
            await _cityService.DeleteCity(id);
            return Ok(new { Status = 200, Message = "City deleted successfully." });
        }
    }
}
