using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shipping.core.Interfaces;
using Shipping.core.Models;
using Shipping.repo.ShippingCon;

namespace Shipping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeightController : ControllerBase
    {
        private readonly IGenericRepo<Weight> _weightRepository;

        public WeightController(IGenericRepo<Weight> weightRepository)
        {
            _weightRepository = weightRepository;
        }

        // GetById
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var weight = await _weightRepository.GetByIdAsync(id);
            if (weight == null)
            {
                return NotFound();
            }
            return Ok(weight);
        }

        // Add
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Weight weight)
        {
            if (weight == null)
            {
                return BadRequest("Invalid data.");
            }

            await _weightRepository.AddAsync(weight);
            //await _weightRepository.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = weight.Id }, weight);
        }
        // Update
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Weight weight)
        {
            if (weight == null || id != weight.Id)
            {
                return BadRequest("Invalid data.");
            }
            var existingWeight = await _weightRepository.GetByIdAsync(id);
            if (existingWeight == null)
            {
                return NotFound();
            }

            existingWeight.DefaultWeight = weight.DefaultWeight;
            existingWeight.AdditionalPrice = weight.AdditionalPrice;

            _weightRepository.UpdateAsync(existingWeight);
            //await _weightRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}